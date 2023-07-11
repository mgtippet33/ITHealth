namespace ITHealth.Domain.Services.Jira.Models;

public class JiraIssueResponse
{
    public int Total { get; set; }
    public List<JiraIssue> Issues { get; set; }
}

public class JiraIssue
{
    public int Id { get; set; }
    public string Self { get; set; }
    public JiraField Fields{ get; set; }
}

public class JiraField
{
    public string Updated { get; set; }
    
    public string Description { get; set; }
    public DateTime UpdatedDate { get; set; }

    public JiraTimeTracking Timetracking { get; set; }
    public JiraUser? Assignee{ get; set; }
}

public class JiraTimeTracking
{
    public int OriginalEstimateSeconds { get; set; }
    public int RemainingEstimateSeconds { get; set; }
    public int TimeSpentSeconds { get; set; }
}
