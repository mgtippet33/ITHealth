namespace ITHealth.Domain.Contracts.Commands.Jira;

public class SetJiraSecretsCommandModel : BaseCommandModel
{
    public string Token { get; set; } = null!;
    public string Url { get; set; } = null!;

    public int TeamId { get; set; }
}