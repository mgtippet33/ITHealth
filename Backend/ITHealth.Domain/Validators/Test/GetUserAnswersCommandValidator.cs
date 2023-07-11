using FluentValidation;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Test
{
    public class GetUserAnswersCommandValidator : AbstractValidator<GetUserAnswersCommandModel>
    {
        private readonly UserManager<User> _userManager;

        public GetUserAnswersCommandValidator(UserManager<User> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;

            RuleFor(x => x.TestId)
                .MustAsync(async (x, cancellation) => await appDbContext.Tests.AnyAsync(t => t.Id == x))
                .WithMessage(x => TestCommandResource.Test_NotExist)
                .OverridePropertyName("Test");

            RuleFor(x => x.UserId)
                .MustAsync(async (x, cancellation) => await appDbContext.Users.AnyAsync(u => u.Id == x))
                .WithMessage(x => CommonResource.User_DoesntExist);

            RuleFor(x => x.CurrentEmail)
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
