using FluentValidation.Results;
using ITHealth.Web.API.Models.Trello.Key;

namespace ITHealth.Web.API.Models.Trello;

public class TrelloSetSecretsResultModel : BaseOperationResultModel<TrelloSetKeyResponseModel>
{
    public TrelloSetSecretsResultModel(TrelloSetKeyResponseModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}
