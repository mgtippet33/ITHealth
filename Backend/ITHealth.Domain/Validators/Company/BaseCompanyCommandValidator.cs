using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Company;
using Microsoft.AspNetCore.Identity;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;
using ITHealth.Data.Enums;

namespace ITHealth.Domain.Validators.Company
{
    public class BaseCompanyCommandValidator : AbstractValidator<BaseCompanyCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public BaseCompanyCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x)
                .MustAsync(async(x, cancellation) => await IsCompanyOwnedByCurrentUserAsync(x))
                .WithMessage(x => CompanyCommandResource.Company_NotExist)
                .OverridePropertyName("Company");

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

        private async Task<bool> IsCompanyOwnedByCurrentUserAsync(BaseCompanyCommandModel command)
        {
            var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
            var role = await GetUserRoleAsync(user);

            return user.CompanyId == command.Id && role == Role.GlobalAdministrator.ToString();
        }

        private async Task<string> GetUserRoleAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.FirstOrDefault().ToString();
        }
    }
}
