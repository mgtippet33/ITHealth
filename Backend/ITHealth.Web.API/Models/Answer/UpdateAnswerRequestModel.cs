using ITHealth.Web.API.Models.Answer;

public class UpdateAnswerRequestModel : AnswerRequestModel
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
}