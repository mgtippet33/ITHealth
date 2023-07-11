namespace ITHealth.Domain.Contracts.Commands.Test;

public class SendTestCommandModel : BaseCommandModel
{
    public int TestId { get; set; }
    public string UserEmail { get; set; }
    public List<UserAnswerCommandModel> UserAnswers { get; set; }
}

public class UserAnswerCommandModel
{
    public int QuestionId { get; set; }
    public int AnswerId { get; set; }
    public List<UserSubAnswerCommandModel> SubAnswers { get; set; }
}

public class UserSubAnswerCommandModel
{
    public int SubquestionId { get; set; }
    public string? Answer { get; set; }
}