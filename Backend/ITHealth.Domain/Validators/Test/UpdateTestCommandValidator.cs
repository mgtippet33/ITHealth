using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Test;

public class UpdateTestCommandValidator : AbstractValidator<UpdateTestCommandModel>
{
    public UpdateTestCommandValidator(AppDbContext appDbContext)
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
            .MustAsync(async (x, cancellation) => await appDbContext.Tests.AnyAsync(t => t.Id == x.Id))
            .WithMessage(x => TestCommandResource.Test_NotExist)
            .OverridePropertyName("Test");
    }
}