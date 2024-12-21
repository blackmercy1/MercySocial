using ErrorOr;
using JetBrains.Annotations;
using MediatR;
using MercySocial.Application.Messages.Services;
using MercySocial.Domain.MessageAggregate;

namespace MercySocial.Application.Messages.Commands;

[UsedImplicitly]
public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, ErrorOr<Message>>
{
    private readonly IMessageService _messageService;

    public SendMessageCommandHandler(
        IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task<ErrorOr<Message>> Handle(
        SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        return await _messageService.CreateMessage(
            request.SenderId,
            request.ReceiverId,
            request.Content,
            cancellationToken);
    }
}