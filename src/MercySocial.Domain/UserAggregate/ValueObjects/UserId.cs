using MercySocial.Domain.common;

namespace MercySocial.Domain.UserAggregate.ValueObjects;

public class UserId : AggregateRootId<int>
{
    public sealed override int Value { get; protected set; }
    
    public UserId(int value)
    {
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
    private UserId() { }
}