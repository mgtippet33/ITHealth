using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Test
{
    public class TestPassedUsersCommandValidator : AbstractValidator<TestPassedUsersCommandModel>
    {
        public TestPassedUsersCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TeamId)
                .MustAsync(async (teamId, cancellation) => await appDbContext.Teams.AnyAsync(t => t.Id == teamId))
                .WithMessage(x => TeamCommandResource.Team_NotExist);

            RuleFor(x => x.TestId)
                .MustAsync(async (testId, cancellation) => await appDbContext.Tests.AnyAsync(t => t.Id == testId))
                .WithMessage(x => TestCommandResource.Test_NotExist);
        }
    }
}
