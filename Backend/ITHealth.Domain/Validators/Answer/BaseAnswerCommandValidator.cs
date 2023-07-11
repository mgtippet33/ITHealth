using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Answer;

public class BaseAnswerCommandValidator : AbstractValidator<BaseAnswerCommandModel>
{
    public BaseAnswerCommandValidator(AppDbContext appDbContext)
    {
        RuleFor(x => x)
            .MustAsync(async (x, cancellation) => await appDbContext.Answers.AnyAsync(t => t.Id == x.Id))
            .WithMessage(x => AnswerCommandResource.Anwer_NotExist)
            .OverridePropertyName("Question");
    }
}