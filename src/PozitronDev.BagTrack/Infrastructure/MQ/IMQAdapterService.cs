using IBM.WMQ;

namespace PozitronDev.BagTrack.Infrastructure.MQ;

public interface IMQAdapterService
{
    bool SendMessageToQueue(string queueName, string objToSend);
    bool SendMessageToTopic(string topicName, string objToSend);

    Task ListenToTopic(string topicName, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken);
    Task ListenToQueue(string queueName, Func<string, CancellationToken, Task> messageHandler, CancellationToken cancellationToken);
}
