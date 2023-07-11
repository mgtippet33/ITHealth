using FluentValidation;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Data.Enums;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Company
{
    public class AcceptUserCompanyCommandValidator : AbstractValidator<AcceptUserCompanyCommandModel>
    {
        private readonly AppDbContext _appDbContext;

        private readonly UserManager<User> _userManager;

        public AcceptUserCompanyCommandValidator(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;

            RuleFor(x => x.CurrentUserEmail)
                .MustAsync(async (x, cancellation) => !await IsTokenExpiredAsync(x))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await IsInviteCodeValidAsync(x))
                .WithMessage(x => CompanyCommandResource.InviteCode_Invalid);

            WhenAsync(async (x, cancellation) => await appDbContext.Companies.AnyAsync(c => c.InviteCode == x.InviteCode) && !await IsSubscribePaidAsync(x), () =>
            {
                RuleFor(x => x)
                    .MustAsync(async (x, cancellation) => !await HasUserLimitAsync(x))
                    .WithMessage(x => CompanyCommandResource.CompanyUserLimit);
            });
        }

        private async Task<bool> IsTokenExpiredAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            var role = await GetUserRoleAsync(user);

            return user == null || role == Role.GlobalAdministrator.ToString();
        }

        private async Task<string> GetUserRoleAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.FirstOrDefault().ToString();
        }
        private async Task<bool> IsInviteCodeValidAsync(AcceptUserCompanyCommandModel command)
        {
            var isInviteCodeValid = await _appDbContext.Companies.AnyAsync(company => company.InviteCode == command.InviteCode);
            var isUserConnectedToCompany = (await _userManager.FindByEmailAsync(command.CurrentUserEmail)).CompanyId.HasValue;

            return !string.IsNullOrEmpty(command.InviteCode) && isInviteCodeValid && !isUserConnectedToCompany;
        }

        private async Task<bool> IsSubscribePaidAsync(AcceptUserCompanyCommandModel command)
        {
            var company = await _appDbContext.Companies.FirstAsync(x => x.InviteCode == command.InviteCode);

            var subscribe = await _appDbContext.SubscribeHistories
                .Where(x => x.CompanyId == company.Id)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            return subscribe != null && subscribe.EndDate >= DateTime.Today.Date;
        }

        private async Task<bool> HasUserLimitAsync(AcceptUserCompanyCommandModel command)
        {
            var company = await _appDbContext.Companies
                .Include(e => e.Users)
                .FirstAsync(x => x.InviteCode == command.InviteCode);

            return company.Users.Count > 5;
        }
    }
}
