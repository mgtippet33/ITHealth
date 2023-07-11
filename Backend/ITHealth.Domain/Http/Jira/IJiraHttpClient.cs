using ITHealth.Domain.Services.Jira.Models;

namespace ITHealth.Domain.Http.Jira;

public interface IJiraHttpClient
{
    Task<JiraBoardResponse> GetCurrentBoardAsync(string email, string token, string url);
    
    Task<JiraIssueResponse> GetCurrentUserIssuesByBoardAsync(int boardId, string email, string token, string url);
}