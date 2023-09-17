namespace PozitronDev.BagTrack.Infrastructure.MQ;

public enum Status
{
    Connected,
    Disconnected
}

public class MQConnectionStatus
{
    public MQConnectionStatus(string queueManager, string topicQueueName, Status status)
    {
        Status = status;
        QueueManager = queueManager;
        TopicQueueName = topicQueueName;
    }

    public Status Status { get; }
    public string QueueManager { get; }
    public string TopicQueueName { get; }

    public override string ToString()
    {
        return $"IBM MQ Adapter Connection Status Changed, Status: {Status}, QueueManager: {QueueManager}, TopicQueueName: {TopicQueueName}.";
    }
}
