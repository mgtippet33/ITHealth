using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Clockify.Key;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Clockify
{
    public class SetClockifySecretsCommandValidator : AbstractValidator<SetClockifySecretsCommandModel>
    {
        private readonly AppDbContext _appDbContext;

        public SetClockifySecretsCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(ClockifyResource.Token_Empty);

            RuleFor(x => x.WorkspaceName)
                .NotEmpty()
                .WithMessage(ClockifyResource.WorkspaceName_Empty);

            RuleFor(x => x.ProjectName)
                .NotEmpty()
                .WithMessage(ClockifyResource.ProjectName_Empty);

            RuleFor(x => x.TeamId)
                .NotEmpty()
                .WithMessage(ClockifyResource.TeamId_Empty);

            RuleFor(x => x)
                .MustAsync(async (x, cancellation) => await IsTeamExistAsync(x))
                .WithMessage(x => TeamCommandResource.Team_NotExist)
                .OverridePropertyName("TeamId");
        }

        private async Task<bool> IsTeamExistAsync(SetClockifySecretsCommandModel command)
        {
            var team = await _appDbContext.Teams.SingleOrDefaultAsync(x => x.Id == command.TeamId);

            return team != null;
        }
    }
}
