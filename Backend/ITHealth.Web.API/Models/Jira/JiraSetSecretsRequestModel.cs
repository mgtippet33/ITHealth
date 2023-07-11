namespace ITHealth.Web.API.Models.Jira;

public class JiraSetSecretsRequestModel
{
    public string Token { get; set; } = null!;
    public string Url { get; set; } = null!;

    public int TeamId { get; set; }
}