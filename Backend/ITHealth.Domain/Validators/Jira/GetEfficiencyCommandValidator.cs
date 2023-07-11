using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Jira;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Health
{
    public class GetEfficiencyCommandValidator : AbstractValidator<GetUserEfficiencyCommandModel>
    {
        public GetEfficiencyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserId)
                .MustAsync(async (userId, cancellation) => await appDbContext.Users.AnyAsync(u => u.Id == userId))
                .WithMessage(x => CommonResource.User_DoesntExist);

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(x => CommonResource.Date_Empty);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(CommonResource.Date_Empty);
        }
    }
}
