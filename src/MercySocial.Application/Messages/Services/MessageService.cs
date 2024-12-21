using MercySocial.Application.Messages.Repository;
using MercySocial.Application.Users.Repository;
using ErrorOr;
using MercySocial.Domain.Common.Errors;
using MercySocial.Domain.MessageAggregate;
using MercySocial.Domain.UserAggregate.ValueObjects;

namespace MercySocial.Application.Messages.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public MessageService(
        IMessageRepository messageRepository,
        IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Message>> CreateMessage(
        Guid senderId,
        Guid receiverId,
        string content,
        CancellationToken cancellationToken)
    {
        var senderExists = await _userRepository.ExistsByIdAsync(UserId.Create(senderId), cancellationToken);
        if (!senderExists)
            return Errors.User.SenderWasNotFound;

        var receiverExists = await _userRepository.ExistsByIdAsync(UserId.Create(receiverId), cancellationToken);
        if (!receiverExists)
            return Errors.User.ReceiverWasNotFound;

        var message = Message.Create(
            id: null,
            senderId: UserId.Create(senderId),
            receiverId: UserId.Create(receiverId),
            content: content);

        await _messageRepository.AddAsync(message, cancellationToken);

        return message;
    }
}