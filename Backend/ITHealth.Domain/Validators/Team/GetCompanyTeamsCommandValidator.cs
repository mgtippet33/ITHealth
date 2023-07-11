using FluentValidation;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;

namespace ITHealth.Domain.Validators.Team
{
    public class GetCompanyTeamsCommandValidator : AbstractValidator<GetCompanyTeamsCommandModel>
    {
        public GetCompanyTeamsCommandValidator(UserManager<User> userManager)
        {
            RuleFor(x => x)
            .MustAsync(async (x, cancellation) => (await userManager.FindByEmailAsync(x.CurrentUserEmail)).CompanyId.HasValue)
            .WithMessage(x => CommonResource.User_IsNotConnectedCompany)
            .OverridePropertyName("User");
        }
    }
}
