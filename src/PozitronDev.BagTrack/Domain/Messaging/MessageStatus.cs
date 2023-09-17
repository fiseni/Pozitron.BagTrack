using System.Runtime.CompilerServices;

namespace PozitronDev.BagTrack.Domain.Messaging;

public class MessageStatus : BaseEnum<MessageStatus>
{
    public static readonly MessageStatus New = new(1);
    public static readonly MessageStatus Processing = new(2);
    public static readonly MessageStatus Processed = new(3);
    public static readonly MessageStatus Failed = new(4);

    private MessageStatus(int value, [CallerMemberName] string name = "")
        : base(value, name)
    {
    }
}
