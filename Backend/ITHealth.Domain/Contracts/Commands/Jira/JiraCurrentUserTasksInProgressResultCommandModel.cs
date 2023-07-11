using FluentValidation.Results;
using ITHealth.Domain.Services.Jira.Models;

namespace ITHealth.Domain.Contracts.Commands.Jira;

public class
    JiraCurrentUserTasksInProgressResultCommandModel : BaseCommandModelResult<JiraCurrentUserTasksInProgressCommandModel>
{
    public JiraCurrentUserTasksInProgressResultCommandModel(JiraCurrentUserTasksInProgressCommandModel data = null,
        ValidationResult validationResult = null) : base(data, validationResult)
    {
    }
}

public class JiraCurrentUserTasksInProgressCommandModel
{
    public List<JiraIssueResponse> JiraIssues { get; set; }
}

public class JiraIssueResponse
{
    public string Self { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public JiraBoard Board { get; set; }
    public JiraTimeTracking TimeTracking { get; set; }
}