using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.UserTeam;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.UserTeam
{
    public class UserTeamCommandValidator : AbstractValidator<UserTeamCommandModel>
    {
        protected readonly AppDbContext _appDbContext;

        protected readonly UserManager<User> _userManager;

        public UserTeamCommandValidator(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;

            RuleFor(x => x.UserEmail)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.Email_Empty);

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await DoesTeamExistAsync(x))
                .WithMessage(x => TeamCommandResource.Team_NotExist)
                .OverridePropertyName("TeamId");

            RuleFor(x => x.UserEmail)
                .MustAsync(async (x, cancellation) => await DoesUserExistAsync(x))
                .WithMessage(x => CommonResource.Email_IsNotRegistred);

            RuleFor(x => x.CurrentUserEmail)
                .MustAsync(async (x, cancellation) => !await IsTokenExpiredAsync(x))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");
        }

        private async Task<bool> IsTokenExpiredAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user == null;
        }

        private async Task<bool> DoesUserExistAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email) != null;
        }

        private async Task<bool> DoesTeamExistAsync(UserTeamCommandModel command)
        {
            var team = await _appDbContext.Teams.SingleOrDefaultAsync(x => x.Id == command.TeamId);

            return team != null;
        }
    }
}
