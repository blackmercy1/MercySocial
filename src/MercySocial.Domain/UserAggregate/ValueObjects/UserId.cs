using MercySocial.Domain.Common;

namespace MercySocial.Domain.UserAggregate.ValueObjects;

public class UserId : AggregateRootId<Guid>
{
    public sealed override Guid Value { get; protected set; }
    
    private UserId(Guid value) => Value = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static UserId Create(Guid value) => new(value);
    public static UserId CreateUniqueUserId() => new(System.Guid.NewGuid());
}