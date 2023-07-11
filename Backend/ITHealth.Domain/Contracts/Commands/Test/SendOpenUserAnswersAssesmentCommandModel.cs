namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class SendOpenUserAnswersAssesmentCommandModel : BaseCommandModel
    {
        public int UserId { get; set; }

        public int TestId { get; set; }

        public List<int> AdditionalScores { get; set; } = new();
    }
}
