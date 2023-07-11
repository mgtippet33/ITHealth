namespace ITHealth.Data.Entities
{
    public class Answer : BaseEntity
    {
        public int QuestionId { get; set; }
        public string Text { get; set; } = null!;
        public int Weight { get; set; }
        public DateTime UpdateDateTime { get; set; } = DateTime.UtcNow;

        public Question Question { get; set; } = new Question();
        public HashSet<UserAnswer> UserAnswers { get; set; } = new();
    }
}
