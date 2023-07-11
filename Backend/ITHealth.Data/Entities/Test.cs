namespace ITHealth.Data.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime UpdateDateTime { get; set; } = DateTime.UtcNow;

        public Company Company { get; set; } = new();
        public HashSet<Question> Questions { get; set; } = new HashSet<Question>();
        public HashSet<TestResult> TestResults { get; set; } = new HashSet<TestResult>();
        public HashSet<TestDeadline> TestDeadlines { get; set; } = new HashSet<TestDeadline>();
    }
}
