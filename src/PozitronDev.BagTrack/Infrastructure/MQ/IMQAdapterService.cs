using IBM.WMQ;

namespace PozitronDev.BagTrack.Infrastructure.MQ;

public interface IMQAdapterService
{
    bool SendMessageToQueue<T>(string queueName, T objToSend);
    bool SendMessageToTopic<T>(string topicName, T objToSend);

    Task ListenToTopic(string topicString, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken);
    Task ListenToQueue(string queueName, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken);
}
