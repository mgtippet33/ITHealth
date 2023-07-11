using ITHealth.Domain.Exceptions;
using ITHealth.Domain.Services.Jira.Models;

namespace ITHealth.Domain.Http.Jira;

public class JiraHttpClient : BaseHttpClient, IJiraHttpClient
{
    
    public JiraHttpClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory.CreateClient("Jira"))
    {
    }

    public async Task<JiraBoardResponse> GetCurrentBoardAsync(string email, string token, string url)
    {
        url += $"board";
        var result = await ExecuteGetRequestAsync<JiraBoardResponse, JiraApiException>(url, GetAuthorizationHeader(email, token));
        return result;
    }

    public async Task<JiraIssueResponse> GetCurrentUserIssuesByBoardAsync(int boardId, string email, string token, string url)
    {
        url += $"board/{boardId}/issue";
        var result = await ExecuteGetRequestAsync<JiraIssueResponse, JiraApiException>(url, GetAuthorizationHeader(email, token));
        return result;
    }

    private RequestHeader GetAuthorizationHeader(string email, string token)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(email + ":" + token);
        return new RequestHeader("Authorization", $"Basic {Convert.ToBase64String(plainTextBytes)}");
    }
}