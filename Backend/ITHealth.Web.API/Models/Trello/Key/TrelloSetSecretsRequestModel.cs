namespace ITHealth.Web.API.Models.Trello.Key;

public class TrelloSetSecretsRequestModel
{
    public string AppKey { get; set; } = null!;

    public int TeamId { get; set; }
}