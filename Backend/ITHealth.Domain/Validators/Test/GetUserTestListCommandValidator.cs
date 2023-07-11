using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Test
{
    public class GetUserTestListCommandValidator : AbstractValidator<GetUserTestListCommandModel>
    {
        private readonly UserManager<User> _userManager;
        public GetUserTestListCommandValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.CurrentPageNumber)
                .GreaterThan(0)
                .WithMessage(x => TestCommandResource.CurrentPageNumber_LessThan);

            RuleFor(x => x.TestCount)
                .GreaterThan(0)
                .WithMessage(x => TestCommandResource.TestCount_LessThan);

            RuleFor(x => x.UserEmail)
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
