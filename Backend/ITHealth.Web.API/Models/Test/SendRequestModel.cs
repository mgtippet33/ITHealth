namespace ITHealth.Web.API.Models.Test;

public class SendRequestModel
{
    public int TestId { get; set; }
    public List<UserAnswerRequestModel> UserAnswers { get; set; }
}

public class UserAnswerRequestModel
{
    public int QuestionId { get; set; }
    public int? AnswerId { get; set; }
    public List<UserSubAnswerRequestModel> SubAnswers { get; set; }
}

public class UserSubAnswerRequestModel
{
    public int SubquestionId { get; set; }
    public string? Answer { get; set; }
}