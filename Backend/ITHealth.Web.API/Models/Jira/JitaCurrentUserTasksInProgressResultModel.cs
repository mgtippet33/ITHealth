using ITHealth.Domain.Services.Jira.Models;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace ITHealth.Web.API.Models.Jira;

public class JiraCurrentUserTasksInProgressResultModel : BaseOperationResultModel<JiraCurrentUserTasksInProgressResponseModel>
{
    public JiraCurrentUserTasksInProgressResultModel(JiraCurrentUserTasksInProgressResponseModel data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}

public class JiraCurrentUserTasksInProgressResponseModel
{
    public List<JiraIssueResponse> JiraIssues { get; set; }
}

public class JiraIssueResponse
{
    public string Self { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public JiraBoard Board{ get; set; }
}