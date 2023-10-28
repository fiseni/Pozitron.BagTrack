namespace PozitronDev.BagTrack.Domain.Messaging;

public class InboxMessage
{
#pragma warning disable CS8618
    private InboxMessage() { }
#pragma warning restore CS8618

    public Guid Id { get; private set; }
    public MessageType Type { get; private set; }
    public MessageStatus Status { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? ProcessedDate { get; private set; }
    public string Data { get; private set; }

    public InboxMessage(IDateTime dateTime, string data, MessageType? messageType = null)
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
