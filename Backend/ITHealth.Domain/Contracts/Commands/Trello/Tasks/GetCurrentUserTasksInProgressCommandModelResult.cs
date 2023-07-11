using FluentValidation.Results;
using ITHealth.Domain.Services.Trello.Models;

namespace ITHealth.Domain.Contracts.Commands.Trello.Tasks;

public class
    GetCurrentUserTasksInProgressCommandModelResult : BaseCommandModelResult<GetCurrentUserTasksInProgressCommandModel>
{
    public GetCurrentUserTasksInProgressCommandModelResult(GetCurrentUserTasksInProgressCommandModel data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
        
    }
}