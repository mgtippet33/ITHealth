using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Health;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Health
{
    public class GetUserStressLevelsCommandValidator : AbstractValidator<GetUserStressLevelsCommandModel>
    {
        public GetUserStressLevelsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage(x => CommonResource.Date_Empty);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage(CommonResource.Date_Empty);

            RuleFor(x => x.CurrentUserEmail)
                .MustAsync(async (x, cancellation) => await appDbContext.Users.AnyAsync(u => u.Email == x))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");
        }
    }
}
