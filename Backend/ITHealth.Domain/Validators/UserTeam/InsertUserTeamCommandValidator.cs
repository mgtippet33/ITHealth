using ITHealth.Data.Entities;
using ITHealth.Data;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using ITHealth.Domain.Resources.Validator;
using ITHealth.Domain.Contracts.Commands.UserTeam;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.UserTeam
{
    public class InsertUserTeamCommandValidator : UserTeamCommandValidator
    {
        public InsertUserTeamCommandValidator(AppDbContext appDbContext, UserManager<User> userManager) : base(appDbContext, userManager)
        {
            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => !await DoesUserExistAsync(x))
                .WithMessage(x => TeamCommandResource.User_Exists)
                .OverridePropertyName("UserEmail");

            RuleFor(x => x.UserEmail)
                .MustAsync(async (email, cancellation) => (await _userManager.FindByEmailAsync(email)).IsActive)
                .WithMessage(x => TeamCommandResource.User_NotActive);
        }

        private async Task<bool> DoesUserExistAsync(UserTeamCommandModel command)
        {
            var team = await _appDbContext.Teams.Include(e => e.Users).SingleOrDefaultAsync(x => x.Id == command.TeamId);
            var user = team?.Users.SingleOrDefault(x => x.Email == command.UserEmail);

            return team != null && user != null;
        }
    }
}
