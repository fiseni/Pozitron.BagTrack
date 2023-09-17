using IBM.WMQ;

namespace PozitronDev.BagTrack.Infrastructure.MQ;

public interface IMQAdapterService
{
    bool SendMessageToQueue(string queueName, object objToSend);
    bool SendMessageToTopic(string topicName, object objToSend);

    Task ListenToTopic(string topicString, Action<MQMessage> messageHandler, Action<MQConnectionStatus> connectionStatusChangedHandler);
    Task ListenToQueue(string queueName, Action<MQMessage> messageHandler, Action<MQConnectionStatus> connectionStatusChangedHandler);
}
