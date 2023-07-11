namespace ITHealth.Domain.Contracts.Commands.Subquestion;

public class SubquestionCommandModel : BaseCommandModel
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;

    public List<UserOpenAnswerCommandModel> UserOpenAnswers { get; set; }
}

public class UserOpenAnswerCommandModel
{
    public string Text { get; set; }
}