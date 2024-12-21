using MercySocial.Domain.Common;
using MercySocial.Domain.MessageAggregate.ValueObjects;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Domain.MessageAggregate;

public class Message : AggregateRoot<MessageId>
{
    public UserId SenderId { get; init; } 
    public UserId ReceiverId { get; init; } 
    public string Content { get; init; } 
    public DateTime SentAt { get; init; } 

    private Message(
        MessageId id,
        UserId senderId,
        UserId receiverId,
        string content,
        DateTime? sentAt = null)
        : base(id)
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        SentAt = sentAt ?? DateTime.UtcNow;
    }

    public static Message Create(
        MessageId? id,
        UserId senderId,
        UserId receiverId,
        string content,
        DateTime? sentAt = null) => new(
        id: id ?? MessageId.CreateUniqueMessageId(),
        senderId: senderId,
        receiverId: receiverId,
        content: content,
        sentAt: sentAt);
    

#pragma warning disable CS8618
    private Message()
    { }
#pragma warning restore CS8618
}