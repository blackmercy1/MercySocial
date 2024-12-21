namespace MercySocial.Presentation.Messages.Requests;

public record SendMessageRequest(
    Guid SenderId,
    Guid ReceiverId,
    string Content);