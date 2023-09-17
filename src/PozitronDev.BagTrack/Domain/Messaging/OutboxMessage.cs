namespace PozitronDev.BagTrack.Domain.Messaging;

public class OutboxMessage : BaseEntity
{
#pragma warning disable CS8618
    private OutboxMessage() { }
#pragma warning restore CS8618

    public MessageType Type { get; private set; }
    public MessageStatus Status { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? ProcessedDate { get; private set; }
    public string Data { get; private set; }

    public OutboxMessage(IDateTime dateTime, string data, MessageType? messageType)
    {
        Type = messageType ?? MessageType.Generic;
        Status = MessageStatus.New;
        CreatedDate = dateTime.UtcNow;
        Data = data;
    }

    public void MarkAsProcessed(IDateTime dateTime)
    {
        Status = MessageStatus.Processed;
        ProcessedDate = dateTime.UtcNow;
    }

    public void MarkAsFailed(IDateTime dateTime)
    {
        Status = MessageStatus.Failed;
        ProcessedDate = dateTime.UtcNow;
    }
}
