using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Jira;

namespace ITHealth.Domain.Contracts.Interfaces;

public interface IJiraService
{
    Task<JiraCurrentUserTasksInProgressResultCommandModel> OpenedUserTaskAsync(string email, DateTime? date = null);

    Task<JiraCurrentUserTasksInProgressResultCommandModel> OpenedUserTaskAsync(string email, int month);

    Task<int> UserTimeInSecondsAsync(string email);

    Task<SetJiraSecretsCommandModelResult> SetJiraSecretsAsync(SetJiraSecretsCommandModel command);
    
    Task<ListEfficiencyCommandModelResult> GetEfficiencyStatisticsAsync(GetUserEfficiencyCommandModel command);

    Task<bool> HasUserLowEfficiencyAsync(string email);

    Task<List<JiraWorkspaceSecrets>> GetSecretsAsync(string email);
}