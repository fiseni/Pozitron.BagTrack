namespace PozitronDev.SharedKernel.Data;

public abstract class BaseEntity : IEntity<Guid>, ISoftDelete
{
    public Guid Id { get; protected set; }
    public bool IsDeleted { get; private set; }

    private readonly List<DomainEvent> _events = new();
    public IEnumerable<DomainEvent> Events => _events.AsEnumerable();

    internal void ClearDomainEvents() => _events.Clear();

    public void RegisterEvent(DomainEvent domainEvent) => _events.Add(domainEvent);
}
