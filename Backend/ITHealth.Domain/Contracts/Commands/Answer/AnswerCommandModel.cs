namespace ITHealth.Domain.Contracts.Commands.Answer
{
    public class AnswerCommandModel : BaseAnswerCommandModel
    {
        public int QuestionId { get; set; }
        public string Text { get; set; } = null!;
        public int Weight { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }
}
