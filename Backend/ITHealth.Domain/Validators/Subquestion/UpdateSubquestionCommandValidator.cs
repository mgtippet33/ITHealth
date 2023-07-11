using FluentValidation;
using ITHealth.Data;
using ITHealth.Domain.Contracts.Commands.Subquestion;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Subquestion;

public class UpdateSubquestionCommandValidator : AbstractValidator<UpdateSubquestionCommandModel>
{
    public UpdateSubquestionCommandValidator(AppDbContext appDbContext)
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage(x => AnswerCommandResource.Text_Exists);

        RuleFor(x => x.QuestionId)
            .NotEqual(0)
            .WithMessage(x => AnswerCommandResource.QuestionId_NotEmpty);

        RuleFor(x => x)
            .MustAsync(async (x, cancellation) => await appDbContext.Subquestions.AnyAsync(t => t.Id == x.Id))
            .WithMessage(x => AnswerCommandResource.Anwer_NotExist)
            .OverridePropertyName("Question");
    }
}