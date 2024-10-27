namespace MercySocial.Domain.Common;

public abstract class Entity<TId> : 
    IEquatable<Entity<TId>>,
    IHasDomainEvents
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public TId Id { get; protected init; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity(TId id) => Id = id;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) 
            return false;
        if (ReferenceEquals(this, obj)) 
            return true;
        
        return obj.GetType() == this.GetType() && Equals((Entity<TId>) obj);
    }
    
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
    public bool Equals(Entity<TId>? other) => Equals((object?) other);
    public override int GetHashCode() => Id.GetHashCode();

#pragma warning disable CS8618
    protected Entity() { }
#pragma warning restore CS8618
}