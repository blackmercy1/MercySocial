namespace MercySocial.Presentation.Messages.Responses;

public record MessageResponse(
    Guid MessageId,
    Guid SenderId,
    Guid ReceiverId,
    string Content,
    DateTime SentAt);