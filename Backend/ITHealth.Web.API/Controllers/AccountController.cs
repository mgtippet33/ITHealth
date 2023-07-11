using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Account.ChangeUserRole;
using ITHealth.Domain.Contracts.Commands.Account.GenerateResetPasswordToken;
using ITHealth.Domain.Contracts.Commands.Account.InviteUser;
using ITHealth.Domain.Contracts.Commands.Account.Login;
using ITHealth.Domain.Contracts.Commands.Account.Profile;
using ITHealth.Domain.Contracts.Commands.Account.ResetPassword;
using ITHealth.Domain.Contracts.Commands.Account.SignUp;
using ITHealth.Domain.Contracts.Commands.Account.Update;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Account;
using ITHealth.Web.API.Models.Account.ChangeUserRole;
using ITHealth.Web.API.Models.Account.DeleteUser;
using ITHealth.Web.API.Models.Account.GenerateResetPasswordToken;
using ITHealth.Web.API.Models.Account.InviteUser;
using ITHealth.Web.API.Models.Account.Login;
using ITHealth.Web.API.Models.Account.ResetPassword;
using ITHealth.Web.API.Models.Account.SignUp;
using ITHealth.Web.API.Models.Account.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITHealth.Web.API.Controllers
{
    [Localization]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<LoginUserResultModel> Login([FromBody] LoginUserRequestModel formModel)
        {
            var command = CreateCommand<LoginUserCommandModel, LoginUserRequestModel>(formModel);
            var commandResult = await _accountService.LoginAsync(command);
            var loginFormModel = commandResult.IsValid
                ? _mapper.Map<LoginUserResponseModel>(commandResult.Data)
                : new LoginUserResponseModel();

            return new LoginUserResultModel(loginFormModel, commandResult.ValidationResult);
        }

        [HttpPost]
        public async Task<UserResultModel> SignUp([FromBody] SignUpUserRequestModel formModel)
        {
            var command = CreateCommand<SignUpUserCommandModel, SignUpUserRequestModel>(formModel);
            var commandResult = await _accountService.SignUpAsync(command);
            var readModel = commandResult.IsValid
                ? _mapper.Map<UserResponseModel>(command)
                : new UserResponseModel();

            return new UserResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize]
        [HttpPut]
        public async Task<UpdateUserResultModel> Update([FromBody] UpdateUserRequestModel formModel)
        {
            var command = CreateCommand<UpdateUserCommandModel, UpdateUserRequestModel>(formModel);
            command.OldEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _accountService.UpdateAsync(command);
            var readModel = commandResult.IsValid
                ? _mapper.Map<UserResponseModel>(commandResult.Data)
                : new UserResponseModel();

            return new UpdateUserResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize]
        [HttpGet]
        public async Task<UserResultModel> Profile()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            var commandResult = await _accountService.GetProfileAsync(userEmail);
            var readModel = commandResult.IsValid
                ? _mapper.Map<UserResponseModel>(commandResult.Data)
                : new UserResponseModel();

            return new UserResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "Administrator, GlobalAdministrator")]
        [HttpGet("{userId}")]
        public async Task<UserResultModel> UserProfile(int userId)
        {
            var command = new GetUserProfileCommandModel
            {
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!,
                UserId = userId
            };

            var commandResult = await _accountService.GetUserProfileAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<UserResponseModel>(commandResult.Data)
                : new UserResponseModel();

            return new UserResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpPost]
        public async Task<GenerateResetPasswordTokenResultModel> GenerateResetPasswordToken([FromBody] GenerateResetPasswordTokenRequestModel formModel)
        {
            var command = CreateCommand<GenerateResetPasswordTokenCommandModel, GenerateResetPasswordTokenRequestModel>(formModel);
            var commandResult = await _accountService.GenerateResetPasswordTokenAsync(command);
            var readModel = commandResult.IsValid
                ? _mapper.Map<GenerateResetPasswordTokenResponseModel>(commandResult.Data)
                : new GenerateResetPasswordTokenResponseModel();

            return new GenerateResetPasswordTokenResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpPost]
        public async Task<ResetPasswordResultModel> ResetPassword([FromBody] ResetPasswordRequestModel formModel)
        {
            var command = CreateCommand<ResetPasswordCommandModel, ResetPasswordRequestModel>(formModel);
            var commandResult = await _accountService.ResetPasswordAsync(command);
            var readModel = commandResult.IsValid
                ? _mapper.Map<ResetPasswordResponseModel>(commandResult.Data)
                : new ResetPasswordResponseModel();

            return new ResetPasswordResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpPost]
        public async Task<InviteUserResultModel> InviteAdministrator([FromBody] InviteUserRequestModel formModel)
        {
            var command = CreateCommand<InviteUserCommandModel, InviteUserRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _accountService.InviteAdministratorAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<InviteUserResponseModel>(commandResult.Data)
                : new InviteUserResponseModel();

            return new InviteUserResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpPost]
        public async Task<ChangeUserRoleResultModel> ChangeUserRole([FromBody] ChangeUserRoleRequestModel formModel)
        {
            var command = CreateCommand<ChangeUserRoleCommandModel, ChangeUserRoleRequestModel>(formModel);

            var commandResult = await _accountService.ChangeUserRoleAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<ChangeUserRoleResponseModel>(commandResult.Data)
                : new ChangeUserRoleResponseModel();

            return new ChangeUserRoleResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpDelete("{userId}")]
        public async Task<DeleteUserResultModel> Delete(int userId)
        {
            var commandResult = await _accountService.DeleteUserAsync(userId);

            var readModel = commandResult.IsValid
                ? _mapper.Map<DeleteUserResponseModel>(commandResult.Data)
                : new DeleteUserResponseModel();

            return new DeleteUserResultModel(readModel, commandResult.ValidationResult);
        }
    }
}
