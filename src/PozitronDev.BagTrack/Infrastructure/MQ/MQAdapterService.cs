using IBM.WMQ;
using System.Collections;

namespace PozitronDev.BagTrack.Infrastructure.MQ;

public class MQAdapterService : IMQAdapterService
{
    private const string CONNECTED = "Connected";
    private const string DISCONNECTED = "Disconnected";

    private Hashtable _queueManagerProperties;
    private readonly MQSettings _mqSettings;
    private readonly ILogger<MQAdapterService> _logger;

    public MQAdapterService(MQSettings mqSettings, ILogger<MQAdapterService> logger)
    {
        _mqSettings = mqSettings;
        _logger = logger;

        _queueManagerProperties = new Hashtable
        {
            { MQC.TRANSPORT_PROPERTY, MQC.TRANSPORT_MQSERIES_MANAGED },
            { MQC.HOST_NAME_PROPERTY, _mqSettings.HostName },
            { MQC.PORT_PROPERTY, _mqSettings.Port },
            { MQC.CHANNEL_PROPERTY, _mqSettings.Channel },
            { MQC.USER_ID_PROPERTY, _mqSettings.UserId },
            { MQC.PASSWORD_PROPERTY, _mqSettings.Password }
        };
    }

    public bool SendMessageToQueue(string queueName, string data)
    {
        try
        {
            using (var queueManager = new MQQueueManager(_mqSettings.QueueManagerName, _queueManagerProperties))
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
            using (var queueManager = new MQQueueManager(_mqSettings.QueueManagerName, _queueManagerProperties))
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

    public Task ListenToTopic(string topicString, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken)
    {
        return ListenToMq(_mqSettings.PollingInterval, _mqSettings.QueueManagerName, topicString, false, messageHandler, cancellationToken);
    }

    public Task ListenToQueue(string queueName, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken)
    {
        return ListenToMq(_mqSettings.PollingInterval, _mqSettings.QueueManagerName, queueName, true, messageHandler, cancellationToken);
    }

    private async Task ListenToMq(
        int pollingInterval,
        string queueManagerName,
        string topicOrQueueString,
        bool isQueue,
        Func<string, CancellationToken, Task> messageHandler,
        CancellationToken cancellationToken)
    {
        var openOptionsForGet = MQC.MQSO_CREATE | MQC.MQSO_FAIL_IF_QUIESCING | MQC.MQSO_MANAGED | MQC.MQSO_NON_DURABLE;
        MQDestination inboundDestination;

        var gmo = new MQGetMessageOptions();
        gmo.Options |= MQC.MQGMO_NO_WAIT | MQC.MQGMO_FAIL_IF_QUIESCING;

        try
        {
            using (var queueManager = new MQQueueManager(queueManagerName, _queueManagerProperties))
            {
                if (isQueue)
                {
                    using (inboundDestination = queueManager.AccessQueue(topicOrQueueString, MQC.MQTOPIC_OPEN_AS_SUBSCRIPTION))
                    {
                        _logger.LogInformation("IBM MQ Adapter Connection Status Changed, Status: {ConnectionStatus}, QueueManager: {QueueManager}, TopicQueueName: {TopicQueueName}.",
                            queueManagerName, topicOrQueueString, CONNECTED);
                        await GetMessageLoop(messageHandler, inboundDestination, gmo, pollingInterval, cancellationToken);
                    }
                }
                else
                {
                    using (inboundDestination = queueManager.AccessTopic(topicOrQueueString, null, MQC.MQTOPIC_OPEN_AS_SUBSCRIPTION, openOptionsForGet))
                    {
                        _logger.LogInformation("IBM MQ Adapter Connection Status Changed, Status: {ConnectionStatus}, QueueManager: {QueueManager}, TopicQueueName: {TopicQueueName}.",
                            queueManagerName, topicOrQueueString, CONNECTED);
                        await GetMessageLoop(messageHandler, inboundDestination, gmo, pollingInterval, cancellationToken);
                    }
                }
            }
        }
        catch (MQException mqException)
        {
            _logger.LogInformation("IBM MQ Adapter Connection Status Changed, Status: {ConnectionStatus}, QueueManager: {QueueManager}, TopicQueueName: {TopicQueueName}.",
                queueManagerName, topicOrQueueString, DISCONNECTED);
            _logger.LogError(mqException, "Error accessing the queue.");
        }
    }

    private async Task GetMessageLoop(
        Func<string, CancellationToken, Task> messageHandler,
        MQDestination inboundDestination,
        MQGetMessageOptions gmo,
        int pollingInterval,
        CancellationToken cancellationToken)
    {
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
                    _logger.LogError(mqException, "Error getting or processing the messages.");
                    throw;
                }
            }
        }
    }
}
