using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account.Login;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public LoginUserCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x)
                .NotNull()
                .MustAsync(async (x, cancellation) => await IsPasswordCorrectAsync(x))
                .WithMessage(x => LoginUserCommandResource.Login)
                .OverridePropertyName("Login");
        }

        private async Task<bool> IsPasswordCorrectAsync(LoginUserCommandModel command)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == command.Email);

            return user != null && user.IsActive &&
                _userManager
                .PasswordHasher
                .VerifyHashedPassword(user, user.PasswordHash, command.Password) == PasswordVerificationResult.Success;
        }
    }
}
