using ErrorOr;
using MediatR;
using MercySocial.Domain.MessageAggregate;

namespace MercySocial.Application.Messages.Commands;

public record SendMessageCommand(
    Guid SenderId,
    Guid ReceiverId,
    string Content) : IRequest<ErrorOr<Message>>;