using IBM.WMQ;
using Serilog;
using System.Collections;

namespace PozitronDev.BagTrack.Infrastructure.MQ;

public class MQAdapterService : IMQAdapterService
{
    private const string CONNECTED = "Connected";
    private const string DISCONNECTED = "Disconnected";

    private readonly MQSettings _mqSettings;
    private readonly ILogger<MQAdapterService> _logger;

    public MQAdapterService(MQSettings mqSettings, ILogger<MQAdapterService> logger)
    {
        _mqSettings = mqSettings;
        _logger = logger;
    }

    public bool SendMessageToQueue(string queueName, string data)
    {
        try
        {
            var queueManagerProperties = GetQueueManagerProperties();
            using (var queueManager = new MQQueueManager(_mqSettings.QueueManagerName, queueManagerProperties))
            {
                using (var outboundTopic = queueManager.AccessQueue(queueName, MQC.MQOO_INPUT_AS_Q_DEF + MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING))
                {
                    var msg = new MQMessage { Persistence = MQC.MQPER_PERSISTENCE_AS_TOPIC_DEF };
                    msg.WriteString(data);
                    outboundTopic.Put(msg);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in sending message to the queue {queueName}.", queueName);
            return false;
        }
    }

    public bool SendMessageToTopic(string topicName, string data)
    {
        try
        {
            var queueManagerProperties = GetQueueManagerProperties();
            using (var queueManager = new MQQueueManager(_mqSettings.QueueManagerName, queueManagerProperties))
            {
                using (var outboundTopic = queueManager.AccessTopic(topicName, null, MQC.MQTOPIC_OPEN_AS_PUBLICATION, MQC.MQOO_OUTPUT))
                {
                    var msg = new MQMessage { Persistence = MQC.MQPER_PERSISTENCE_AS_TOPIC_DEF };
                    msg.WriteString(data);
                    outboundTopic.Put(msg);
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in sending message to the topic {topicName}.", topicName);
            return false;
        }
    }

    public async Task ListenToTopic(string topicString, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken)
    {
        var queueManagerProperties = GetQueueManagerProperties();
        var queueManagerName = _mqSettings.QueueManagerName;
        var pollingInterval = _mqSettings.PollingInterval;

        try
        {
            var openOptionsForGet = MQC.MQSO_CREATE | MQC.MQSO_FAIL_IF_QUIESCING | MQC.MQSO_MANAGED | MQC.MQSO_NON_DURABLE;

            using (var queueManager = new MQQueueManager(queueManagerName, queueManagerProperties))
            {
                _logger.LogInformation("IBM MQ Connected to QueueManager: {QueueManager}", queueManagerName);

                using (var inboundDestination = queueManager.AccessTopic(topicString, null, MQC.MQTOPIC_OPEN_AS_SUBSCRIPTION, openOptionsForGet))
                {
                    _logger.LogInformation("IBM MQ Adapter Connection Status Changed, Status: {ConnectionStatus}, QueueManager: {QueueManager}, TopicName: {TopicName}.", CONNECTED, queueManagerName, topicString);
                    await GetMessageLoop(messageHandler, inboundDestination, pollingInterval, _logger, cancellationToken);
                }
            }
        }
        catch (MQException mqException)
        {
            _logger.LogInformation("IBM MQ Adapter Connection Status Changed, Status: {ConnectionStatus}, QueueManager: {QueueManager}, TopicName: {TopicName}.", DISCONNECTED, queueManagerName, topicString);
            _logger.LogError(mqException, "Error accessing the topic.");
        }
    }

    public async Task ListenToQueue(string queueName, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken)
    {
        var queueManagerProperties = GetQueueManagerProperties();
        var queueManagerName = _mqSettings.QueueManagerName;
        var pollingInterval = _mqSettings.PollingInterval;

        try
        {
            _logger.LogInformation("IBM MQ Trying to connect to QueueManager: {QueueManager}, MQSettings: {@mqSettings}", queueManagerName, _mqSettings);

            using (var queueManager = new MQQueueManager(queueManagerName, queueManagerProperties))
            {
                _logger.LogInformation("IBM MQ Connected to QueueManager: {QueueManager}", queueManagerName);

                using (var inboundDestination = queueManager.AccessQueue(queueName, MQC.MQOO_INPUT_AS_Q_DEF))
                {
                    _logger.LogInformation("IBM MQ Adapter Connection Status Changed, Status: {ConnectionStatus}, QueueManager: {QueueManager}, QueueName: {QueueName}.", CONNECTED, queueManagerName, queueName);

                    await GetMessageLoop(messageHandler, inboundDestination, pollingInterval, _logger, cancellationToken);
                }
            }
        }
        catch (MQException mqException)
        {
            _logger.LogInformation("IBM MQ Adapter Connection Status Changed, Status: {ConnectionStatus}, QueueManager: {QueueManager}, QueueName: {QueueName}.", DISCONNECTED, queueManagerName, queueName);
            _logger.LogError(mqException, "Error accessing the queue.");
        }
    }

    private static async Task GetMessageLoop(
        Func<string, CancellationToken, Task> messageHandler,
        MQDestination inboundDestination,
        int pollingInterval,
        ILogger<MQAdapterService> logger,
        CancellationToken cancellationToken)
    {
        var gmo = new MQGetMessageOptions();
        gmo.Options |= MQC.MQGMO_NO_WAIT | MQC.MQGMO_FAIL_IF_QUIESCING;

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var message = new MQMessage();
                inboundDestination.Get(message, gmo);
                var data = message.ReadString(message.DataLength);

                await messageHandler(data, cancellationToken);
            }
            catch (MQException mqException)
            {
                if (mqException.Reason == MQC.MQRC_NO_MSG_AVAILABLE)
                {
                    await Task.Delay(pollingInterval, cancellationToken);
                }
                else
                {
                    logger.LogError(mqException, "Error getting or processing the messages.");
                    throw;
                }
            }
        }
    }

    private Hashtable GetQueueManagerProperties()
    {
        var queueManagerProperties = new Hashtable();

        queueManagerProperties.Add("TransportType", "TCP");
        queueManagerProperties.Add(MQC.TRANSPORT_PROPERTY, MQC.TRANSPORT_MQSERIES_MANAGED);

        if (!string.IsNullOrEmpty(_mqSettings.HostName))
            queueManagerProperties.Add(MQC.HOST_NAME_PROPERTY, _mqSettings.HostName.Trim());

        queueManagerProperties.Add(MQC.PORT_PROPERTY, _mqSettings.Port);

        if (!string.IsNullOrEmpty(_mqSettings.Channel))
            queueManagerProperties.Add(MQC.CHANNEL_PROPERTY, _mqSettings.Channel.Trim());

        if (!string.IsNullOrEmpty(_mqSettings.UserId))
        {
            queueManagerProperties.Add(MQC.USER_ID_PROPERTY, _mqSettings.UserId.Trim());
        }

        if (!string.IsNullOrEmpty(_mqSettings.Password))
        {
            queueManagerProperties.Add(MQC.PASSWORD_PROPERTY, _mqSettings.Password.Trim());
        }

        if (!string.IsNullOrEmpty(_mqSettings.CCSID))
        {
            queueManagerProperties.Add(MQC.CCSID_PROPERTY, _mqSettings.CCSID.Trim());
        }

        return queueManagerProperties;
    }
}
