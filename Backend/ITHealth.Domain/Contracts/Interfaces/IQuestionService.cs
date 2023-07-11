using ITHealth.Domain.Contracts.Commands.Question;

namespace ITHealth.Domain.Contracts.Interfaces;

public interface IQuestionService
{
    Task<QuestionCommandModelResult> GetQuestionByIdAsync(int questionId);

    Task<QuestionListCommandModelResult> GetQuestionsAsync(int? testId = null);
}