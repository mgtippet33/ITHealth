using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Data.Enums;
using ITHealth.Domain.Contracts.Commands.Account.SignUp;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class SignUpUserCommandValidator : AbstractValidator<SignUpUserCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public SignUpUserCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.Email_Empty);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.Password_Empty);

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.Role_Empty);

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.FullName_Empty);

            RuleFor(x => x.Email)
                .MustAsync(async (x, cancellation) => !await IsUserRegistredByEmailAsync(x))
                .WithMessage(x => UserCommandResource.Email_Registred);

            RuleFor(x => x.Role)
                .Must(x => IsRoleCorrect(x))
                .WithMessage(x => UserCommandResource.Role_IsNotCorrect);
        }

        private async Task<bool> IsUserRegistredByEmailAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            return !string.IsNullOrEmpty(email) && user != null;
        }

        private bool IsRoleCorrect(string role)
        {
            return Enum.GetNames(typeof(Role)).Any(x => x == role);
        }
    }
}
