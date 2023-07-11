using AutoMapper;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Question;
using ITHealth.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services;

public class QuestionService : BaseApplicationService, IQuestionService
{
    public QuestionService(UserManager<User> userManager, AppDbContext appDbContext, IServiceProvider serviceProvider, IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
    {
    }

    public async Task<QuestionCommandModelResult> GetQuestionByIdAsync(int questionId)
    {
        var question = await _appDbContext.Questions
            .Include(e => e.Answers)
            .Include(e => e.Subquestions)
            .SingleOrDefaultAsync(e => e.Id == questionId);
        
        var testCommand = _mapper.Map<QuestionCommandModel>(question);

        return new QuestionCommandModelResult(testCommand);
    }

    public async Task<QuestionListCommandModelResult> GetQuestionsAsync(int? testId = null)
    {
        IQueryable<Question> questionsQuery = _appDbContext.Questions
            .Include(e => e.Answers)
            .Include(e => e.Subquestions);
        
        if (testId.HasValue)
        {
            questionsQuery = questionsQuery.Where(e => e.TestId == testId);
        }

        var questions = await questionsQuery.ToListAsync();
        var command = _mapper.Map<List<QuestionCommandModel>>(questions);
            
        return new QuestionListCommandModelResult(command);
    }
}