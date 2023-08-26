using MediatR;

namespace PozitronDev.SharedKernel.Data;

public abstract class DomainEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = Clock.Now;
}
