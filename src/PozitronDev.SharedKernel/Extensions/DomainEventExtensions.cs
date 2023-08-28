using MediatR;

namespace PozitronDev.SharedKernel.Extensions;

public static class DomainEventExtensions
{
    public static async Task DispatchAndClearEvents(this IEnumerable<BaseEntity> entities, IMediator mediator)
    {
        foreach (var entity in entities)
        {
            var events = entity.Events.ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
