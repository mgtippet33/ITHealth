using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Health;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Health
{
    public class HealthCommandValidator : AbstractValidator<HealthCommandModel>
    {
        protected readonly AppDbContext _appDbContext;

        protected readonly UserManager<User> _userManager;

        public HealthCommandValidator(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;

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
    }
}
