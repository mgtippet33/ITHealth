namespace ITHealth.Data.Entities
{
    public class TestDeadline : BaseEntity
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public DateTime Deadline { get; set; }

        public User User { get; set; }
        public Test Test { get; set; }
    }
}
