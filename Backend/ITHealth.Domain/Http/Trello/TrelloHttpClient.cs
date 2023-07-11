using ITHealth.Domain.Exceptions;
using ITHealth.Domain.Services.Trello.Models;

namespace ITHealth.Domain.Http.Trello;

public class TrelloHttpClient : BaseHttpClient, ITrelloHttpClient
{
    public TrelloHttpClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory.CreateClient("Trello"))
    {
    }

    public async Task<List<TrelloBoard>> ListUserBoardsAsync(string key, string token)
    {
        var url = $"members/me/boards?key={key}&token={token}";
        return await ExecuteGetRequestAsync<List<TrelloBoard>, TrelloApiException>(url);
    }
    
    public async Task<List<TrelloCard>> ListBoardCardsAsync(string key, string token, string boardShortLink)
    {
        var url = $"boards/{boardShortLink}/cards?key={key}&token={token}";
        return await ExecuteGetRequestAsync<List<TrelloCard>, TrelloApiException>(url);
    }

    public async Task<TrelloMember> GetCurrentUserAsync(string key, string token)
    {
        var url = $"members/me?key={key}&token={token}";
        return await ExecuteGetRequestAsync<TrelloMember, TrelloApiException>(url); 
    }
    
    public async Task<List<TrelloCard>> ListUserCardsAsync(string key, string token, string memberId)
    {
        var url = $"members/{memberId}/cards?key={key}&token={token}";
        return await ExecuteGetRequestAsync<List<TrelloCard>, TrelloApiException>(url);
    }
}