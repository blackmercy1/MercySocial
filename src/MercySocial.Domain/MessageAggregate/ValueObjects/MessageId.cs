using MercySocial.Domain.Common;

namespace MercySocial.Domain.MessageAggregate.ValueObjects;

public class MessageId : AggregateRootId<Guid>
{
    public sealed override Guid Value { get; protected set; }
    
    private MessageId(Guid value) => Value = value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static MessageId Create(Guid value) => new(value);
    public static MessageId CreateUniqueMessageId() => new(Guid.NewGuid());
}