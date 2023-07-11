using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;

namespace ITHealth.Domain.Validators.Test;

public class CreateTestCommandValidator : AbstractValidator<CreateTestCommandModel>
{
    public CreateTestCommandValidator(UserManager<User> userManager)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(x => TestCommandResource.Name_Exists);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(x => TestCommandResource.Description_Exists);

        RuleFor(x => x.TestResults)
            .Null()
            .WithMessage(x => TestCommandResource.TestResults_Empty);

        RuleFor(x => x.Questions)
            .NotEmpty()
            .WithMessage(x => TestCommandResource.Questions_NotEmpty);

        RuleFor(x => x)
            .MustAsync(async (x, cancellation) => (await userManager.FindByEmailAsync(x.CurrentUserEmail)).CompanyId.HasValue)
            .WithMessage(x => CommonResource.User_IsNotConnectedCompany)
            .OverridePropertyName("User");
    }
}