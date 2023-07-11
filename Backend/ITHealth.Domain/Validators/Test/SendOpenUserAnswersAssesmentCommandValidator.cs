using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Test
{
    public class SendOpenUserAnswersAssesmentCommandValidator : AbstractValidator<SendOpenUserAnswersAssesmentCommandModel>
    {
        public SendOpenUserAnswersAssesmentCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .MustAsync(async (x, cancellation) => await appDbContext.Tests.AnyAsync(t => t.Id == x))
                .WithMessage(x => TestCommandResource.Test_NotExist);

            RuleFor(x => x.UserId)
                .MustAsync(async (x, cancellation) => await appDbContext.Users.AnyAsync(t => t.Id == x))
                .WithMessage(x => CommonResource.User_DoesntExist);
        }
    }
}
