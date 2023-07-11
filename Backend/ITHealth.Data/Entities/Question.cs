namespace ITHealth.Data.Entities
{
    public class Question : BaseEntity
    {
        public int TestId { get; set; }
        public int Number { get; set; }
        public string Description { get; set; } = null!;
        public DateTime UpdateDateTime { get; set; } = DateTime.UtcNow;

        public Test Test { get; set; } = new Test();
        public HashSet<Answer> Answers { get; set; } = new HashSet<Answer>();
        public HashSet<Subquestion> Subquestions { get; set; } = new HashSet<Subquestion>();
    }
}
