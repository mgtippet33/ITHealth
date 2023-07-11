using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Test;

public class TestCommandValidator : AbstractValidator<BaseTestCommandModel>
{
    public TestCommandValidator(AppDbContext appDbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellation) => await appDbContext.Tests.AnyAsync(t => t.Id == x.Id))
            .WithMessage(x => TestCommandResource.Test_NotExist)
            .OverridePropertyName("Test");
    }
}