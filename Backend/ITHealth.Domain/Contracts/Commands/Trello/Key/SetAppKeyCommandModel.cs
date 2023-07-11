namespace ITHealth.Domain.Contracts.Commands.Trello.Key;

public class SetAppKeyCommandModel : BaseCommandModel
{
    public string AppKey { get; set; } = null!;
    public int TeamId { get; set; }
}