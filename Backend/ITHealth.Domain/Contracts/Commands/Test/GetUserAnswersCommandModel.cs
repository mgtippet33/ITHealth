namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class GetUserAnswersCommandModel : BaseCommandModel
    {
        public int TestId { get; set; }

        public int UserId { get; set; }

        public string CurrentEmail { get; set; } = null!;
    }
}
