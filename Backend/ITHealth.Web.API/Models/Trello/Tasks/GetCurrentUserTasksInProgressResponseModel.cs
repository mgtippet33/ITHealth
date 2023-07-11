using FluentValidation.Results;
using ITHealth.Domain.Services.Trello.Models;

namespace ITHealth.Web.API.Models.Trello.Tasks;

public class
    GetCurrentUserTasksInProgressResultModel : BaseOperationResultModel<GetCurrentUserTasksInProgressResponseModel>
{
    public GetCurrentUserTasksInProgressResultModel(GetCurrentUserTasksInProgressResponseModel data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}

public class GetCurrentUserTasksInProgressResponseModel
{
    public List<TrelloCard> TrelloCards { get; set; }
}