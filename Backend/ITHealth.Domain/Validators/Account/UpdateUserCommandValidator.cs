using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account.Update;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.Email_Empty);

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.FullName_Empty);

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => !await IsUserRegistredByEmailAsync(x))
                .WithMessage(x => UserCommandResource.Email_Registred)
                .OverridePropertyName("Email");

            RuleFor(x => x.OldEmail)
                .MustAsync(async (x, cancellation) => !await IsTokenExpiredAsync(x))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");
        }

        private async Task<bool> IsUserRegistredByEmailAsync(UpdateUserCommandModel command)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == command.Email);

            return user != null && user.Email != command.OldEmail;
        }

        private async Task<bool> IsTokenExpiredAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user == null;
        }
    }
}
