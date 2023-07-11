using ITHealth.Web.API.Models.Answer;
using ITHealth.Web.API.Models.Subquestion;

namespace ITHealth.Web.API.Models.Question;

public class QuestionRequestModel : BaseQuestionModel
{
    public List<AnswerRequestModel> Answers { get; set; } = null;
    public List<SubquestionRequestModel> Subquestions { get; set; } = null;
}

public class BaseQuestionModel
{
    public int Number { get; set; }
    public string Description { get; set; } = null!;
}