using ITHealth.Domain.Contracts.Commands.Answer;

namespace ITHealth.Domain.Contracts.Interfaces;

public interface IAnswerService
{
    public Task<AnswerListCommandModelResult> GetAnswersByQuestionAsync(int questionId);

    public Task<AnswerCommandModelResult> UpdateAnswerAsync(UpdateAnswerCommandModel command);

    public Task<AnswerCommandModelResult> DeleteAnswerAsync(BaseAnswerCommandModel command);
}