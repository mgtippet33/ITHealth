using FluentValidation;
using ITHealth.Data.Entities;
using Microsoft.AspNetCore.Identity;
using ITHealth.Domain.Resources.Validator;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Company
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommandModel>
    {
        protected readonly UserManager<User> _userManager;

        public UpdateCompanyCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(x => CompanyCommandResource.Name_Empty);

            RuleFor(x => x.CurrentUserEmail)
                .MustAsync(async (x, cancellation) => !await IsTokenExpiredAsync(x))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => (await _userManager.FindByEmailAsync(x.CurrentUserEmail)).CompanyId.HasValue)
                .WithMessage(x => CompanyCommandResource.Company_NotExist)
                .OverridePropertyName("Name");
        }

        private async Task<bool> IsTokenExpiredAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            var role = await GetUserRoleAsync(user);

            return user == null || role != Role.GlobalAdministrator.ToString();
        }

        private async Task<string> GetUserRoleAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.FirstOrDefault().ToString();
        }
    }
}
