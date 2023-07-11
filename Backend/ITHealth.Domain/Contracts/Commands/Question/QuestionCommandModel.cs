using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Contracts.Commands.Subquestion;

namespace ITHealth.Domain.Contracts.Commands.Question;

public class QuestionCommandModel : BaseCommandModel 
{
    public int Id { get; set; }
    public int Number { get; set; }
    public string Description { get; set; } = null!;
    
    public List<AnswerCommandModel> Answers { get; set; }
    public List<SubquestionCommandModel> Subquestions { get; set; }
}