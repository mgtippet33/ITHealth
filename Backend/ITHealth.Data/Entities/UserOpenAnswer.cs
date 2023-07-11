namespace ITHealth.Data.Entities;

public class UserOpenAnswer : BaseEntity
{
    public string Text { get; set; }
    public int UserId { get; set; }
    public int SubquestionId { get; set; }

    public User User { get; set; } 
    public Subquestion Subquestion { get; set; } 
}