using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Team
{
    public class TeamCommandValidator : AbstractValidator<TeamCommandModel>
    {
        protected readonly AppDbContext _appDbContext;

        protected readonly UserManager<User> _userManager;

        public TeamCommandValidator(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(x => TeamCommandResource.Name_Empty);
            
            RuleFor(x => x.CurrentUserEmail)
                .MustAsync(async (x, cancellation) => !await IsTokenExpiredAsync(x))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => (await userManager.FindByEmailAsync(x.CurrentUserEmail)).CompanyId.HasValue)
                .WithMessage(x => CommonResource.User_IsNotConnectedCompany)
                .OverridePropertyName("User");
        }

        private async Task<bool> IsTokenExpiredAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user == null;
        }
    }
}
