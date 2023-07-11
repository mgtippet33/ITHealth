namespace ITHealth.Domain.Services.Trello.Models;

public class TrelloCard
{
    public string TrelloId { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public List<string> IdMembers { get; set; }
    public bool Closed { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? Due { get; set; }

}