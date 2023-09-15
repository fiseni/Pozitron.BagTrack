using PozitronDev.SharedKernel.Data;
using System.Runtime.CompilerServices;

namespace PozitronDev.BagTrack.Domain.Messaging;

public class MessageType : BaseEnum<MessageType>
{
    public static readonly MessageType Generic = new(1);
    public static readonly MessageType CarouselAdded = new(2);
    public static readonly MessageType CarouselRemoved = new(3);

    private MessageType(int value, [CallerMemberName] string name = "")
        : base(value, name)
    {
    }
}
