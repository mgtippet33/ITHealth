using ITHealth.Web.API.Models.Answer;
using ITHealth.Web.API.Models.Subquestion;

namespace ITHealth.Web.API.Models.Question;

public class QuestionResponseModel : BaseQuestionModel
{
    public int Id { get; set; }

    public List<AnswerResponseModel> Answers { get; set; } = null;
    public List<SubquestionResponseModel> Subquestions { get; set; } = null;
}
