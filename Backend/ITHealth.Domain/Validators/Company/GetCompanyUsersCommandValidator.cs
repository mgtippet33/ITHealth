using FluentValidation;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Company
{
    public class GetCompanyUsersCommandValidator : AbstractValidator<GetCompanyUsersCommandModel>
    {
        public GetCompanyUsersCommandValidator(UserManager<User> userManager, AppDbContext appDbContext)
        {
            RuleFor(x => x.CurrentUserEmail)
                .MustAsync(async (email, cancellation) => await userManager.Users.AnyAsync(x => x.Email == email))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");

            RuleFor(x => x.ExcludingTeamId)
                .MustAsync(async (teamId, cancellation) => await appDbContext.Teams.AnyAsync(x => x.Id == teamId))
                .WithMessage(x => TeamCommandResource.Team_NotExist);
        }
    }
}
