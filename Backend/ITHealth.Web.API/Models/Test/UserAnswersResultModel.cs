using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Test
{
    public class UserAnswersResultModel : BaseOperationResultModel<UserAnswersResponseModel>
    {
        public UserAnswersResultModel(UserAnswersResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class UserAnswersResponseModel
    {
        public int UserId { get; set; }

        public int TestId { get; set; }

        public List<QuestionUserAnswersResponseModel> Questions { get; set; } = new();
    }

    public class QuestionUserAnswersResponseModel
    {
        public int QuestionId { get; set; }

        public string QuestionDescription { get; set; } = string.Empty;

        public List<TestAnswerResponseModel>? Answers { get; set; }

        public string? UserOpenAnswer { get; set; }
    }

    public class TestAnswerResponseModel
    {
        public int AnswerId { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsChoosen { get; set; }
    }
}
