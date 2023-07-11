using ITHealth.Domain.Contracts.Commands.Account;
using ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole;
using ITHealth.Domain.Contracts.Commands.Account.DeleteUser;
using ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken;
using ITHealth.Domain.Contracts.Commands.Account.InviteUser;
using ITHealth.Domain.Contracts.Commands.Account.Login;
using ITHealth.Domain.Contracts.Commands.Account.Profile;
using ITHealth.Domain.Contracts.Commands.Account.ResetPassword;
using ITHealth.Domain.Contracts.Commands.Account.SignUp;
using ITHealth.Domain.Contracts.Commands.Account.Update;

namespace ITHealth.Domain.Contracts.Interfaces
{
    public interface IAccountService
    {
        Task<LoginUserCommandModelResult> LoginAsync(LoginUserCommandModel command);

        Task<SignUpUserCommandModelResult> SignUpAsync(SignUpUserCommandModel command);

        Task<UpdateUserCommandModelResult> UpdateAsync(UpdateUserCommandModel command);

        Task<UserCommandModelResult> GetProfileAsync(string email);

        Task<UserCommandModelResult> GetUserProfileAsync(GetUserProfileCommandModel command);

        Task<GenerateResetPasswordTokenCommandModelResult> GenerateResetPasswordTokenAsync(GenerateResetPasswordTokenCommandModel command);

        Task<ResetPasswordCommandModelResult> ResetPasswordAsync(ResetPasswordCommandModel command);

        Task<InviteUserCommandModelResult> InviteAdministratorAsync(InviteUserCommandModel command);

        Task<ChangeUserRoleCommandModelResult> ChangeUserRoleAsync(ChangeUserRoleCommandModel command);

        Task<DeleteUserCommandModelResult> DeleteUserAsync(int userId);
    }
}
