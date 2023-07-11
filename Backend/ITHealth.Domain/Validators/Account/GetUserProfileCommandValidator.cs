using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account.Profile;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class GetUserProfileCommandValidator : AbstractValidator<GetUserProfileCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public GetUserProfileCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await _userManager.Users.AnyAsync(u => u.Id == x.UserId) && await AreUsersOnSameCompany(x))
                .WithMessage(x => CommonResource.User_DoesntExist)
                .OverridePropertyName(x => x.UserId);
        }

        private async Task<bool> AreUsersOnSameCompany(GetUserProfileCommandModel command)
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == command.CurrentUserEmail);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == command.UserId);

            return currentUser != null && user != null && currentUser.CompanyId == user.CompanyId;
        }
    }
}
