using FluentValidation;
using JetBrains.Annotations;

namespace MercySocial.Application.Messages.Commands;

[UsedImplicitly]
public class SendMessageCommandValidator : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.SenderId)
            .NotNull();

        RuleFor(x => x.ReceiverId)
            .NotNull()
            .NotEqual(x => x.SenderId);

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000);
    }
}