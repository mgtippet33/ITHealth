namespace ITHealth.Data.Entities;

public class BaseWorkspaceSecrets
{
    public int TeamId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Team Team { get; set; } = new Team();
}