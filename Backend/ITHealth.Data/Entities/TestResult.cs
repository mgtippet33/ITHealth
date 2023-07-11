namespace ITHealth.Data.Entities
{
    public class TestResult: BaseEntity
    {
        public int UserId { get; set; }
        public int TestId { get; set; }
        public int Result { get; set; }
        public int MaxResult { get; set; }
        public bool IsVerified { get; set; }

        public User User { get; set; }
        public Test Test { get; set; } 
    }
}
