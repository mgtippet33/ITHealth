using FluentValidation;
using ITHealth.Data;
using ITHealth.Data.Enums;
using ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole;
using ITHealth.Domain.Resources.Validator;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Validators.Account
{
    public class ChangeUserRoleCommandValidator : AbstractValidator<ChangeUserRoleCommandModel>
    {
        public ChangeUserRoleCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserId)
                .MustAsync(async (x, cancellation) => await appDbContext.Users.AnyAsync(user => user.Id == x))
                .WithMessage(x => CommonResource.User_DoesntExist);

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage(x => UserCommandResource.Role_Empty);

            RuleFor(x => x.Role)
                .Must(x => Enum.GetNames(typeof(Role)).Any(role => x == role))
                .WithMessage(x => UserCommandResource.Role_IsNotCorrect);
        }
    }
}
