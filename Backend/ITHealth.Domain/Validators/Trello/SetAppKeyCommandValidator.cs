using FluentValidation;
using ITHealth.Domain.Contracts.Commands.Trello.Key;
using ITHealth.Domain.Resources.Validator;

namespace ITHealth.Domain.Validators.Trello;

public class SetAppKeyCommandValidator : AbstractValidator<SetAppKeyCommandModel>
{
    public SetAppKeyCommandValidator()
    {
        RuleFor(x => x.AppKey)
            .NotEmpty()
            .WithMessage(AppKeyCommandResource.AppKey_Empty);
    }
}