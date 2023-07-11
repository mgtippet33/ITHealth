namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class UserTestingStatisticsCommandModel : BaseCommandModel
    {
        public int UserId { get; set; }

        public int TestId { get; set; }
    }
}
