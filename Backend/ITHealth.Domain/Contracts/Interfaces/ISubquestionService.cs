using ITHealth.Domain.Contracts.Commands.Subquestion;

namespace ITHealth.Domain.Contracts.Interfaces;

public interface ISubquestionService
{
    public Task<SubquestionListCommandModelResult> GetSubquestionByQuestionAsync(int questionId);

    public Task<SubquestionCommandModelResult> UpdateSubquestionAsync(UpdateSubquestionCommandModel command);
}