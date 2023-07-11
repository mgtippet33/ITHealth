using AutoMapper;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Subquestion;
using ITHealth.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services;

public class SubquestionService : BaseApplicationService, ISubquestionService
{
    public SubquestionService(UserManager<User> userManager, AppDbContext appDbContext, IServiceProvider serviceProvider, IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
    {
    }

    public async Task<SubquestionListCommandModelResult> GetSubquestionByQuestionAsync(int questionId)
    {
        var answers = await _appDbContext.Subquestions
            .Include(e => e.Question)
            .Include(e => e.UserOpenAnswers)
            .Where(x => x.QuestionId == questionId)
            .ToListAsync();
        var answersCommand = _mapper.Map<List<SubquestionCommandModel>>(answers);
        
        return new SubquestionListCommandModelResult(answersCommand);
    }

    public async Task<SubquestionCommandModelResult> UpdateSubquestionAsync(UpdateSubquestionCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var answer = await _appDbContext.Subquestions
                .Include(e => e.Question)
                .SingleAsync(x => x.Id == command.Id);
            var updatedAnswer = _mapper.Map(command, answer);

            _appDbContext.Subquestions.Update(updatedAnswer);
            await _appDbContext.SaveChangesAsync();

            command.Id = updatedAnswer.Id;
        }

        return new SubquestionCommandModelResult(command, validationResult);
    }
}