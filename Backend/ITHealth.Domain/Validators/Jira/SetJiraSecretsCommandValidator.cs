using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Jira;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Jira;

public class SetJiraSecretsCommandValidator : AbstractValidator<SetJiraSecretsCommandModel>
{
    private readonly AppDbContext _appDbContext;

    public SetJiraSecretsCommandValidator(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;

        RuleFor(x => x.Token)
            .NotEmpty()
            .WithMessage(JiraResource.Token_Empty);

        RuleFor(x => x.TeamId)
            .NotEmpty()
            .WithMessage(JiraResource.TeamId_Empty);
        
        RuleFor(x => x.Url)
            .NotEmpty()
            .WithMessage(JiraResource.TeamId_Empty);

        RuleFor(x => x)
            .MustAsync(async (x, cancellation) => await IsTeamExistAsync(x))
            .WithMessage(x => TeamCommandResource.Team_NotExist)
            .OverridePropertyName("TeamId");
    }

    private async Task<bool> IsTeamExistAsync(SetJiraSecretsCommandModel command)
    {
        var team = await _appDbContext.Teams.SingleOrDefaultAsync(x => x.Id == command.TeamId);

        return team != null;
    }
}