namespace ITHealth.Domain.Services.Jira.Models;

public class JiraBoardResponse
{
    public int Total { get; set; }
    public bool IsLast { get; set; }
    public List<JiraBoard> Values { get; set; }
}

public class JiraBoard
{
    public int Id { get; set; }
    public string Self { get; set; }
    public string Name { get; set; }
    public Location Location { get; set; }
}

public class Location
{
    public int ProjectId { get; set; }
    public string DisplayName { get; set; }
    public string ProjectKey { get; set; }
    public string AvatarURI { get; set; }
}
