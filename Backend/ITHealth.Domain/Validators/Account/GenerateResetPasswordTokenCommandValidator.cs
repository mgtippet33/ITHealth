using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class GenerateResetPasswordTokenCommandValidator : AbstractValidator<GenerateResetPasswordTokenCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public GenerateResetPasswordTokenCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(x => CommonResource.Email_Empty);

            RuleFor(x => x.Email)
                .MustAsync(async (x, cancellation) => await DoesUserExistAsync(x))
                .WithMessage(x => CommonResource.Email_IsNotRegistred);
        }

        public async Task<bool> DoesUserExistAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email) != null;
        }
    }
}
