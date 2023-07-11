using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Account;
using ITHealth.Domain.Contracts.Interfaces;
using Microsoft.AspNetCore.Identity;
using ITHealth.Data.Entities;
using ITHealth.Domain.Contracts.Commands.Account.Login;
using ITHealth.Domain.Contracts.Commands.Account.SignUp;
using ITHealth.Domain.Contracts.Commands.Account.Update;
using ITHealth.Domain.Contracts.Commands.Account.ResetPassword;
using ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken;
using ITHealth.Data;
using Microsoft.EntityFrameworkCore;
using ITHealth.Domain.Contracts.Commands.Account.InviteUser;
using ITHealth.Data.Enums;
using ITHealth.Domain.Settings;
using ITHealth.Web.API.Infrastructure.Services;
using Microsoft.Extensions.Options;
using ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole;
using ITHealth.Domain.Resources.Validator;
using ITHealth.Domain.Contracts.Commands.Account.DeleteUser;
using ITHealth.Domain.Contracts.Commands.Account.Profile;

namespace ITHealth.Domain.Services
{
    public class AccountService : BaseApplicationService, IAccountService
    {
        private readonly IMailService _mailService;

        private readonly JWTSecuritySettings _jwtSecuritySettings;

        public AccountService(
            UserManager<User> userManager,
            AppDbContext appDbContext,
            IMailService mailService,
            IOptions<JWTSecuritySettings> jwtSecuritySettings,
            IServiceProvider serviceProvider,
            IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
        {
            _mailService = mailService;
            _jwtSecuritySettings = jwtSecuritySettings.Value;
        }

        public async Task<LoginUserCommandModelResult> LoginAsync(LoginUserCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = new LoginUserResponseCommandModel();

            if (validationResult != null && validationResult.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Email == command.Email);
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                responseCommand.Token = JWTSecurityHandler.GenerateLoginToken(_jwtSecuritySettings, user, role);
                responseCommand.Role = role;
            }
            
            return new LoginUserCommandModelResult(responseCommand, validationResult);
        }

        public async Task<SignUpUserCommandModelResult> SignUpAsync(SignUpUserCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var user = _mapper.Map<User>(command);
                var managerResult = await _userManager.CreateAsync(user, command.Password);
                var roleResult = await _userManager.AddToRoleAsync(user, command.Role);

                if (!managerResult.Succeeded || !roleResult.Succeeded)
                {
                    return new SignUpUserCommandModelResult();
                }
            }

            return new SignUpUserCommandModelResult(validationResult);
        }

        public async Task<UpdateUserCommandModelResult> UpdateAsync(UpdateUserCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.Users.FirstAsync(x => x.Email == command.OldEmail);

                var updatedUser = _mapper.Map(command, user);

                var updateResult = await _userManager.UpdateAsync(updatedUser);
                var setPasswordResult = !string.IsNullOrEmpty(command.Password) ?
                    await UpdatePasswordAsync(user, command.Password) :
                    IdentityResult.Success;

                if (!updateResult.Succeeded || !setPasswordResult.Succeeded)
                {
                    return new UpdateUserCommandModelResult();
                }

                command.Id = updatedUser.Id;
                command.Role = await GetUserRoleAsync(user);
            }

            return new UpdateUserCommandModelResult(command, validationResult);
        } 

        public async Task<UserCommandModelResult> GetProfileAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var command = _mapper.Map<UserCommandModel>(user);

            var validationResult = ValidateUserToken(user);
            
            if (validationResult != null && validationResult.IsValid)
            {
                command.Role = await GetUserRoleAsync(user);
            }

            return new UserCommandModelResult(command, validationResult);
        }

        public async Task<UserCommandModelResult> GetUserProfileAsync(GetUserProfileCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = new UserCommandModel();

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.Users.SingleAsync(u => u.Id == command.UserId);

                responseCommand = _mapper.Map<UserCommandModel>(user);
                responseCommand.Role = await GetUserRoleAsync(user);
            }

            return new UserCommandModelResult(responseCommand, validationResult);
        }

        public async Task<GenerateResetPasswordTokenCommandModelResult> GenerateResetPasswordTokenAsync(GenerateResetPasswordTokenCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var commandResponseModel = new GenerateResetPasswordTokenCommandResponseModel();

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.Users.FirstAsync(x => x.Email == command.Email);
                var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                commandResponseModel.IsEmailSent = _mailService.SendResetPasswordTokenMail(command.Email, passwordResetToken);
            }

            return new GenerateResetPasswordTokenCommandModelResult(commandResponseModel, validationResult);
        }

        public async Task<ResetPasswordCommandModelResult> ResetPasswordAsync(ResetPasswordCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var commandResponseModel = new ResetPasswordCommandResponseModel();

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.Users.FirstAsync(x => x.Email == command.Email);
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, command.ResetToken, command.Password);
                commandResponseModel.IsPasswordChanged = resetPasswordResult.Succeeded;
            }

            return new ResetPasswordCommandModelResult(commandResponseModel, validationResult);
        }

        public async Task<InviteUserCommandModelResult> InviteAdministratorAsync(InviteUserCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var commandResponseModel = new InviteUserResponseCommandModel();

            if (validationResult != null && validationResult.IsValid)
            {
                var currentUser = await _userManager.FindByEmailAsync(command.CurrentUserEmail);

                var invitedUser = _mapper.Map<User>(command);
                invitedUser.CompanyId = currentUser.CompanyId;
                invitedUser.Company = await _appDbContext.Companies.SingleAsync(x => x.Id == currentUser.CompanyId);

                var managerResult = await _userManager.CreateAsync(invitedUser);
                var roleResult = await _userManager.AddToRoleAsync(invitedUser, Role.Administrator.ToString());

                if (!managerResult.Succeeded || !roleResult.Succeeded)
                {
                    return new InviteUserCommandModelResult();
                }

                var token = JWTSecurityHandler.GenerateInviteUserToken(_jwtSecuritySettings, invitedUser, Role.Administrator.ToString());
                commandResponseModel.IsEmailSent = validationResult.IsValid
                                                   ? _mailService.SendUserInviteMail(command.InvitedUserEmail, token)
                                                   : false;
            }

            return new InviteUserCommandModelResult(commandResponseModel, validationResult);
        }

        public async Task<ChangeUserRoleCommandModelResult> ChangeUserRoleAsync(ChangeUserRoleCommandModel command)
        {
            var validationResult = await ValidateCommandAsync(command);
            var responseCommand = new ChangeUserRoleResponseCommandModel { IsSuccessful = validationResult?.IsValid ?? false };

            if (validationResult != null && validationResult.IsValid)
            {
                var user = await _userManager.FindByIdAsync(command.UserId.ToString());
                var role = await GetUserRoleAsync(user);

                var removeRoleResult = (await _userManager.RemoveFromRoleAsync(user, role)).Succeeded;
                responseCommand.IsSuccessful = removeRoleResult
                                               ? (await _userManager.AddToRoleAsync(user, command.Role)).Succeeded
                                               : false;
            }

            return new ChangeUserRoleCommandModelResult(responseCommand, validationResult);
        }

        public async Task<DeleteUserCommandModelResult> DeleteUserAsync(int userId) 
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var validationResult = ValidateNullData(user, "User", CommonResource.User_DoesntExist);
            var responseCommand = new DeleteUserResponseCommandModel { IsSuccessful = validationResult.IsValid };

            if (validationResult != null && validationResult.IsValid)
            {
                user = await _appDbContext.Users
                    .Include(e => e.Healths)
                    .Include(e => e.Teams)
                    .Include(e => e.TestResults)
                    .Include(e => e.TestDeadlines)
                    .SingleAsync(x =>  x.Id == userId);

                responseCommand.IsSuccessful = (await _userManager.DeleteAsync(user)).Succeeded;
            }

            return new DeleteUserCommandModelResult(responseCommand, validationResult);
        }

        private async Task<IdentityResult> UpdatePasswordAsync(User user, string password)
        {
            var result = await _userManager.RemovePasswordAsync(user);

            if (result.Succeeded)
            {
                result = await _userManager.AddPasswordAsync(user, password);
            }

            return result;
        }
    }
}
