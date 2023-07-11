using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Company;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Company
{
    public class CompanyCommandValidator : AbstractValidator<CompanyCommandModel>
    {
        protected readonly UserManager<User> _userManager;

        public CompanyCommandValidator(UserManager<User> userManager) 
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
                .MustAsync(async (x, cancellation) => !(await _userManager.FindByEmailAsync(x.CurrentUserEmail)).CompanyId.HasValue)
                .WithMessage(x => CompanyCommandResource.Company_Exist)
                .OverridePropertyName("Name");
        }

        private async Task<bool> IsTokenExpiredAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user == null;
        }
    }
}
