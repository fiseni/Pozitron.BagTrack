namespace PozitronDev.SharedKernel.Data;

public abstract class BaseEntity : IEntity<Guid>, IAuditableEntity, ISoftDelete
{
    public Guid Id { get; protected set; }
    public bool IsDeleted { get; private set; }

    public DateTime? AuditCreatedTime { get; set; }
    public string? AuditCreatedByUserId { get; set; }
    public string? AuditCreatedByUsername { get; set; }
    public DateTime? AuditModifiedTime { get; set; }
    public string? AuditModifiedByUserId { get; set; }
    public string? AuditModifiedByUsername { get; set; }


    private readonly List<DomainEvent> _events = new();
    public IEnumerable<DomainEvent> Events => _events.AsEnumerable();

    internal void ClearDomainEvents() => _events.Clear();

    public void RegisterEvent(DomainEvent domainEvent) => _events.Add(domainEvent);

    public void UpdateCreateInfo(DateTime now, ICurrentUser currentUser)
    {
        AuditCreatedTime = now;
        AuditCreatedByUserId = currentUser?.UserId;
        AuditCreatedByUsername = currentUser?.Username;
    }

    public void UpdateModifyInfo(DateTime now, ICurrentUser currentUser)
    {
        AuditModifiedTime = now;
        AuditModifiedByUserId = currentUser?.UserId;
        AuditModifiedByUsername = currentUser?.Username;
    }
}
