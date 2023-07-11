using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using ITHealth.Domain.Contracts.Commands.Team;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Team
{
    public class BaseTeamCommandValidator : AbstractValidator<BaseTeamCommandModel>
    {
        protected readonly AppDbContext _appDbContext;

        protected readonly UserManager<User> _userManager;

        public BaseTeamCommandValidator(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await DoesTeamExistAsync(x))
                .WithMessage(x => TeamCommandResource.Team_NotExist)
                .OverridePropertyName("Team");

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

        private async Task<bool> DoesTeamExistAsync(BaseTeamCommandModel command)
        {
            var team = await _appDbContext.Teams.SingleOrDefaultAsync(x => x.Id == command.Id);

            return team != null;
        }
    }
}
