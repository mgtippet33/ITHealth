using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account.ResetPassword;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.ResetToken)
                .NotEmpty()
                .WithMessage(x => CommonResource.Token_Empty);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(x => CommonResource.Email_Empty);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(x => CommonResource.Password_Empty);

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await IsResetTokenValidAsync(x))
                .WithMessage(x => CommonResource.ResetPassword_Expired)
                .OverridePropertyName("ResetPassword");
        }

        public async Task<bool> IsResetTokenValidAsync(ResetPasswordCommandModel command)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == command.Email);
            var tokenProvider = _userManager.Options.Tokens.PasswordResetTokenProvider;
            var isUserResetTokenValid = await _userManager.VerifyUserTokenAsync(user, tokenProvider, "ResetPassword", command.ResetToken);

            return user != null && isUserResetTokenValid;
        }
    }
}
