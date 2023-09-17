using MediatR;
using PozitronDev.SharedKernel.Contracts;
using PozitronDev.SharedKernel.Data;

namespace PozitronDev.Extensions.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static void ApplyAuditing(this DbContext dbContext, IDateTime dateTime, ICurrentUser currentUser)
    {
        var addedEntries = dbContext.ChangeTracker.Entries<IAuditableEntity>().Where(x => x.IsAdded());
        var modifiedEntries = dbContext.ChangeTracker.Entries<IAuditableEntity>().Where(x => x.IsModified());

        var now = dateTime.UtcNow;

        foreach (var addedEntry in addedEntries)
        {
            addedEntry.Entity.UpdateCreateInfo(now, currentUser);
        }

        foreach (var modifiedEntry in modifiedEntries)
        {
            modifiedEntry.Entity.UpdateModifyInfo(now, currentUser);
        }
    }

    public static void ApplySoftDelete(this DbContext dbContext)
    {
        var entries = dbContext.ChangeTracker.Entries<ISoftDelete>().Where(x => x.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            //entry.CurrentValues.SetValues(entry.OriginalValues); // TODO: Think about this. What do we want? [fatii, 8/9/2022]
            entry.State = EntityState.Unchanged;
            entry.CurrentValues[nameof(ISoftDelete.IsDeleted)] = true;

            var referenceEntries = entry.References.Where(x => x.TargetEntry is not null &&
                                                                x.TargetEntry.Metadata.IsOwned());

            foreach (var targetEntry in referenceEntries.Select(e => e.TargetEntry))
            {
                if (targetEntry is not null)
                {
                    targetEntry.CurrentValues.SetValues(targetEntry.OriginalValues);
                    targetEntry.State = EntityState.Unchanged;
                }
            }
        }
    }

    public static async Task PublishDomainEvents(this DbContext dbContext, IMediator mediator)
    {
        var entities = dbContext.ChangeTracker.Entries<BaseEntity>().Select(x => x.Entity).Where(x => x.Events.Any()).ToList();

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
