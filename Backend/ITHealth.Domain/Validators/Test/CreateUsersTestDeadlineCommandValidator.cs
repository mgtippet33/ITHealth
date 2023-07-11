using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Test
{
    public class CreateUsersTestDeadlineCommandValidator : AbstractValidator<CreateUsersTestDeadlineCommandModel>
    {
        public CreateUsersTestDeadlineCommandValidator(AppDbContext appDbContext)
        {
            RuleForEach(x => x.UserIds)
                .MustAsync(async (userId, cancellation) => await appDbContext.Users.AnyAsync(u => u.Id == userId))
                .WithMessage(x => CommonResource.User_DoesntExist);

            RuleFor(x => x.TestId)
                .MustAsync(async (testId, cancellation) => await appDbContext.Tests.AnyAsync(t => t.Id == testId))
                .WithMessage(x => TestCommandResource.Test_NotExist);
        }
    }
}
