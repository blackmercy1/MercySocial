using ErrorOr;
using MercySocial.Domain.MessageAggregate;

namespace MercySocial.Application.Messages.Services;

public interface IMessageService
{
    Task<ErrorOr<Message>> CreateMessage(
        Guid senderId,
        Guid receiverId,
        string content,
        CancellationToken cancellationToken);
}