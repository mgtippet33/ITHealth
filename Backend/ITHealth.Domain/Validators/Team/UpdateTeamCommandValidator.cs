using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Team;
using Microsoft.AspNetCore.Identity;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Team
{
    public class UpdateTeamCommandValidator : TeamCommandValidator
    {
        public UpdateTeamCommandValidator(AppDbContext appDbContext, UserManager<User> userManager) : base(appDbContext, userManager)
        {
            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await DoesTeamExistAsync(x))
                .WithMessage(x => TeamCommandResource.Team_NotExist)
                .OverridePropertyName("Team");

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => !await DoesSameTeamExistAsync(x))
                .WithMessage(x => TeamCommandResource.Name_Exists)
                .OverridePropertyName("Name");
        }

        private async Task<bool> DoesSameTeamExistAsync(TeamCommandModel command)
        {
            var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
            var team = await _appDbContext.Teams.SingleOrDefaultAsync(x => x.Id != command.Id && x.Name == command.Name && x.CompanyId == user.CompanyId);

            return team != null;
        }

        private async Task<bool> DoesTeamExistAsync(TeamCommandModel command)
        {
            var team = await _appDbContext.Teams.SingleOrDefaultAsync(x => x.Id == command.Id);

            return team != null;
        }
    }
}
