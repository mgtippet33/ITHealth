using FluentValidation;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account.InviteUser;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class InviteUserCommandValidator : AbstractValidator<InviteUserCommandModel>
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;

        public InviteUserCommandValidator(AppDbContext appDbContext, UserManager<User> userManager)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;

            RuleFor(x => x.InvitedUserEmail)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.Email_Empty);

            RuleFor(x => x.InvitedUserEmail)
                .MustAsync(async (x, cancellation) => !await IsUserRegistredByEmailAsync(x))
                .WithMessage(x => UserCommandResource.Email_Registred);

            RuleFor(x => x.CurrentUserEmail)
                .MustAsync(async (x, cancellation) => !await IsTokenExpiredAsync(x))
                .WithMessage(x => CommonResource.Token_Expired)
                .OverridePropertyName("Token");

            WhenAsync(async (x, cancellation) => !await IsSubscribePaidAsync(x), () =>
            {
                RuleFor(x => x)
                    .MustAsync(async (x, cancellation) => !await HasUserLimitAsync(x))
                    .WithMessage(x => CompanyCommandResource.CompanyUserLimit);
            });
        }

        private async Task<bool> IsUserRegistredByEmailAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            return !string.IsNullOrEmpty(email) && user != null;
        }

        private async Task<bool> IsTokenExpiredAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);

            return user == null;
        }

        private async Task<bool> IsSubscribePaidAsync(InviteUserCommandModel command)
        {
            var companyId = (await _userManager.FindByEmailAsync(command.CurrentUserEmail)).CompanyId;

            var subscribe = await _appDbContext.SubscribeHistories
                .Where(x => x.CompanyId == companyId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync();

            return subscribe != null && subscribe.EndDate >= DateTime.Today.Date;
        }

        private async Task<bool> HasUserLimitAsync(InviteUserCommandModel command)
        {
            var companyId = (await _userManager.FindByEmailAsync(command.CurrentUserEmail)).CompanyId;
            var company = await _appDbContext.Companies
                .Include(e => e.Users)
                .FirstAsync(x => x.Id == companyId);

            return company.Users.Count > 5;
        }
    }
}
