using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Trello.Key;
using ITHealth.Domain.Contracts.Commands.Trello.Tasks;

namespace ITHealth.Domain.Contracts.Interfaces;

public interface ITrelloService
{
    Task<SetAppKeyCommandModelResult> SetAppKeyAsync(SetAppKeyCommandModel command);

    Task<GetCurrentUserTasksInProgressCommandModelResult> OpenedUserTaskAsync(string token, string email);

    Task<List<TrelloWorkspaceSecrets>> GetSecretsAsync(string email);
}