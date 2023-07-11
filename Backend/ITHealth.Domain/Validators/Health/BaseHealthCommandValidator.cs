using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Health;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Health
{
    public class BaseHealthCommandValidator : AbstractValidator<BaseHealthCommandModel>
    {
        protected readonly AppDbContext _appDbContext;

        protected readonly UserManager<User> _userManager;

        public BaseHealthCommandValidator(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await DoesHealthExistAsync(x))
                .WithMessage(x => HealthCommandResource.Health_NotExist)
                .OverridePropertyName("Health");

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

        private async Task<bool> DoesHealthExistAsync(BaseHealthCommandModel command)
        {
            var health = await _appDbContext.Healths.SingleOrDefaultAsync(x => x.Id == command.Id);

            return health != null;
        }
    }
}
