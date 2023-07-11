using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Jira;

public class JiraCurrentUserTimeEntriesResultModel : BaseOperationResultModel<int>
{
    public JiraCurrentUserTimeEntriesResultModel(int data, ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}