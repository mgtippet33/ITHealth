namespace ITHealth.Data.Entities;

public class UserAnswer : BaseEntity
{
    public int UserId { get; set; }
    public int AnswerId { get; set; }

    public User User { get; set; } 
    public Answer Answer { get; set; }
}