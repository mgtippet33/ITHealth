namespace ITHealth.Web.API.Models.Subquestion;

public class UpdateSubquestionRequestModel : SubquestionRequestModel
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
}