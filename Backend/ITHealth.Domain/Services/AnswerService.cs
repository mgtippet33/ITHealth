using AutoMapper;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services;

public class AnswerService : BaseApplicationService, IAnswerService
{
    public AnswerService(UserManager<User> userManager, AppDbContext appDbContext, IServiceProvider serviceProvider, IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
    {
    }

    public async Task<AnswerListCommandModelResult> GetAnswersByQuestionAsync(int questionId)
    {
        var answers = await _appDbContext.Answers
            .Include(e => e.Question)
            .Where(x => x.QuestionId == questionId)
            .ToListAsync();
        var answersCommand = _mapper.Map<List<AnswerCommandModel>>(answers);

        return new AnswerListCommandModelResult(answersCommand, null);
    }

    public async Task<AnswerCommandModelResult> UpdateAnswerAsync(UpdateAnswerCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var answer = await _appDbContext.Answers
                .Include(e => e.Question)
                .SingleAsync(x => x.Id == command.Id);
            var updatedAnswer = _mapper.Map(command, answer);

            _appDbContext.Answers.Update(updatedAnswer);
            await _appDbContext.SaveChangesAsync();

            command.Id = updatedAnswer.Id;
        }

        return new AnswerCommandModelResult(command, validationResult);
    }

    public async Task<AnswerCommandModelResult> DeleteAnswerAsync(BaseAnswerCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var answer = await _appDbContext.Answers.SingleAsync(x => x.Id == command.Id);

            _appDbContext.Answers.Remove(answer);
            await _appDbContext.SaveChangesAsync();
        }

        return new AnswerCommandModelResult(validationResult: validationResult);
    }
}