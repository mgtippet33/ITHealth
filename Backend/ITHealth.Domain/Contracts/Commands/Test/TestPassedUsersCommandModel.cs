namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class TestPassedUsersCommandModel : BaseCommandModel
    {
        public int TeamId { get; set; }

        public int TestId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
