namespace ITHealth.Web.API.Models.Test
{
    public class SendOpenUserAnswersAssesmentRequestModel
    {
        public int UserId { get; set; }

        public int TestId { get; set; }

        public List<int> AdditionalScores { get; set; } = new();
    }
}
