using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data;
using Microsoft.AspNetCore.Identity;
using ITHealth.Domain.Resources.Validator;
using ITHealth.Domain.Contracts.Commands.Team;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Team
{
    public class CreateTeamCommandValidator : TeamCommandValidator
    {
        public CreateTeamCommandValidator(AppDbContext appDbContext, UserManager<User> userManager) : base(appDbContext, userManager)
        {
            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => !await DoesSameTeamExistAsync(x))
                .WithMessage(x => TeamCommandResource.Name_Exists)
                .OverridePropertyName("Name");

            WhenAsync(async (x, cancellation) => !await IsSubscribePaidAsync(x), () =>
            {
                RuleFor(x => x)
                    .MustAsync(async (x, cancellation) => !await HasTeamLimitAsync(x))
                    .WithMessage(x => CompanyCommandResource.CompanyUserLimit);
            });
        }

        private async Task<bool> DoesSameTeamExistAsync(TeamCommandModel command)
        {
            var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
            var team = await _appDbContext.Teams.SingleOrDefaultAsync(x => x.Name == command.Name && x.CompanyId == user.CompanyId);

            return team != null;
        }

        private async Task<bool> IsSubscribePaidAsync(TeamCommandModel command)
        {
            var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
            var company = await _appDbContext.Companies.FirstAsync(x => x.Id == user.CompanyId);

            var subscribe = await _appDbContext.SubscribeHistories
                .Where(x => x.CompanyId == company.Id)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            return subscribe != null && subscribe.EndDate >= DateTime.Today.Date;
        }

        private async Task<bool> HasTeamLimitAsync(TeamCommandModel command)
        {
            var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
            var company = await _appDbContext.Companies
                .Include(e => e.Teams)
                .FirstAsync(x => x.Id == user.CompanyId);

            return company.Teams.Count >= 1;
        }
    }
}
