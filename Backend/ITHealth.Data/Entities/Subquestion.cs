namespace ITHealth.Data.Entities
{
    public class Subquestion: BaseEntity
    {
        public int QuestionId { get; set; }
        public string Text { get; set; } = null!;
        
        public Question Question { get; set; } = new Question();
        public HashSet<UserOpenAnswer> UserOpenAnswers { get; set; } = new();
    }
}
