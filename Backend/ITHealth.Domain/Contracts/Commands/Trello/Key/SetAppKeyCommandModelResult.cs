using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Trello.Key;

public class SetAppKeyCommandModelResult : BaseCommandModelResult<SetAppKeyCommandModel>
{
    public SetAppKeyCommandModelResult(SetAppKeyCommandModel data, ValidationResult validationResult) : base(data, validationResult)
    {
    }
}