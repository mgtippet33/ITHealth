using FluentValidation;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;

namespace ITHealth.Domain.Validators.Test;

public class SendTestCommandValidator : AbstractValidator<SendTestCommandModel>
{
    public SendTestCommandValidator()
    {
        RuleFor(x => x.UserAnswers)
            .ForEach(x => x.NotEmpty())
            .WithMessage(x => TestCommandResource.Name_Exists);
    }
}