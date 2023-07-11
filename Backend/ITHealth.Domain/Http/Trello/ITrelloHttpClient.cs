using ITHealth.Domain.Services.Trello.Models;

namespace ITHealth.Domain.Http.Trello;

public interface ITrelloHttpClient
{
    Task<List<TrelloBoard>> ListUserBoardsAsync(string key, string token);

    Task<List<TrelloCard>> ListBoardCardsAsync(string key, string token, string boardShortLink);

    Task<TrelloMember> GetCurrentUserAsync(string key, string token);

    Task<List<TrelloCard>> ListUserCardsAsync(string key, string token, string memberId);
}