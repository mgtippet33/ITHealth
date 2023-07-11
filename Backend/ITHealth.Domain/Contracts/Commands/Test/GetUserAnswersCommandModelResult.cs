using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class GetUserAnswersCommandModelResult : BaseCommandModelResult<UserAnswersResponseCommandModel>
    {
        public GetUserAnswersCommandModelResult(UserAnswersResponseCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class UserAnswersResponseCommandModel
    {
        public int UserId { get; set; }

        public int TestId { get; set; }

        public List<QuestionUserAnswersResponseCommandModel> Questions { get; set; } = new();
    }

    public class QuestionUserAnswersResponseCommandModel
    {
        public int QuestionId { get; set; }

        public string QuestionDescription { get; set; } = string.Empty;

        public List<TestAnswerResponseCommandModel>? Answers { get; set; }

        public string? UserOpenAnswer { get; set; }
    }

    public class TestAnswerResponseCommandModel
    {
        public int AnswerId { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsChoosen { get; set; }
    }
}
