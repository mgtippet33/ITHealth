using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Jira;

public class SetJiraSecretsCommandModelResult : BaseCommandModelResult<SetJiraSecretsCommandModel>
{
    public SetJiraSecretsCommandModelResult(SetJiraSecretsCommandModel data, ValidationResult validationResult) : base(data, validationResult) 
    {
    }
}