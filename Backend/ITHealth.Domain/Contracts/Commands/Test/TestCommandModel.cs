using ITHealth.Domain.Contracts.Commands.Question;

namespace ITHealth.Domain.Contracts.Commands.Test;

public class TestCommandModel : BaseTestCommandModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool IsActive { get; set; }
    public DateTime UpdateDateTime { get; set; }

    public List<QuestionCommandModel> Questions { get; set; }
    public List<TestResultCommandModel> TestResults { get; set; }
    public List<UserTestDeadlineCommandModel> TestDeadlines { get; set; }
}