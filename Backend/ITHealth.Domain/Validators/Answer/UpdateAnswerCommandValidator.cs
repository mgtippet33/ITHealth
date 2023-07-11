using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Answer;

public class UpdateAnswerCommandValidator : AbstractValidator<UpdateAnswerCommandModel>
{
    public UpdateAnswerCommandValidator(AppDbContext appDbContext)
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage(x => AnswerCommandResource.Text_Exists);

        RuleFor(x => x.Weight)
            .NotEqual(0)
            .WithMessage(x => AnswerCommandResource.Weight_NotZero);

        RuleFor(x => x.QuestionId)
            .NotEqual(0)
            .WithMessage(x => AnswerCommandResource.QuestionId_NotEmpty);

        RuleFor(x => x)
            .MustAsync(async (x, cancellation) => await appDbContext.Answers.AnyAsync(t => t.Id == x.Id))
            .WithMessage(x => AnswerCommandResource.Anwer_NotExist)
            .OverridePropertyName("Question");
    }
}