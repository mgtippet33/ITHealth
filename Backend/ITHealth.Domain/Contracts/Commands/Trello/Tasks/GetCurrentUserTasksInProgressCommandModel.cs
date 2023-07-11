using ITHealth.Domain.Services.Trello.Models;

namespace ITHealth.Domain.Contracts.Commands.Trello.Tasks;

public class GetCurrentUserTasksInProgressCommandModel : BaseCommandModel
{
    public List<TrelloCard> TrelloCards { get; set; }
}