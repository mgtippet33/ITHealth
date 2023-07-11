using FluentValidation.Results;
using ITHealth.Data.Entities;

namespace ITHealth.Web.API.Models.Trello.Key;

public class TrelloGetSecretsResultModel : BaseOperationResultModel<TrelloWorkspaceSecrets>
{
    public TrelloGetSecretsResultModel(TrelloWorkspaceSecrets data = null, ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}
