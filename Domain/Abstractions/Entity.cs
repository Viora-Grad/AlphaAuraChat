namespace AlphaAuraChat.Domain.Abstractions;

public abstract class Entity
{
    public Guid Id { get; init; }
    protected Entity(Guid id)
    {
        Id = id;
    }

    // left for EfCore to map the attributes to the read data
    protected Entity() { }

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}