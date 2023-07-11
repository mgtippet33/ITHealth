using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Jira;

public class JiraSetSecretsResultModel : BaseOperationResultModel<JiraSetSecretsResponseModel>
{
    public JiraSetSecretsResultModel(JiraSetSecretsResponseModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}

public class JiraSetSecretsResponseModel
{
    public bool IsSuccessful { get; set; }
}