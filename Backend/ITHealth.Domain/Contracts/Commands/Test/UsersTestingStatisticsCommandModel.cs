namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class UsersTestingStatisticsCommandModel : BaseCommandModel
    {
        public int TestId { get; set; }

        public List<int> UserIds { get; set; } = new();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
