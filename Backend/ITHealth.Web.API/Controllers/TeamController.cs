using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Team;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Team;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ITHealth.Web.API.Models.UserTeam;
using ITHealth.Domain.Contracts.Commands.UserTeam;
using ITHealth.Web.API.Models.Account;

namespace ITHealth.Web.API.Controllers
{
    [Localization]
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [ApiController]
    public class TeamController : BaseApiController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _teamService = teamService;
        }

        [HttpGet("{teamId}")]
        public async Task<TeamResultModel> Get(int teamId)
        {
            var command = new BaseTeamCommandModel()
            {
                Id = teamId,
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
            };

            var commandResult = await _teamService.GetTeamAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<TeamResponseModel>(commandResult.Data)
                : new TeamResponseModel();

            return new TeamResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpGet]
        public async Task<TeamListResultModel> Get()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _teamService.GetUserTeamsAsync(currentUserEmail);

            var readModel = commandResult.IsValid
                ? _mapper.Map<List<TeamResponseModel>>(commandResult.Data)
                : new List<TeamResponseModel>();

            return new TeamListResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpGet]
        public async Task<TeamListResultModel> GetCompanyTeams()
        {
            var command = new GetCompanyTeamsCommandModel()
            {
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!
            };

            var commandResult = await _teamService.GetCompanyTeamsAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<List<TeamResponseModel>>(commandResult.Data)
                : new List<TeamResponseModel>();

            return new TeamListResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpPost]
        public async Task<TeamResultModel> Create([FromBody] TeamRequestModel formModel)
        {
            var command = CreateCommand<CreateTeamCommandModel, TeamRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _teamService.CreateTeamAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<TeamResponseModel>(commandResult.Data)
                : new TeamResponseModel();

            return new TeamResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpPut]
        public async Task<TeamResultModel> Update([FromBody] UpdateTeamRequestModel formModel)
        {
            var command = CreateCommand<UpdateTeamCommandModel, UpdateTeamRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _teamService.UpdateTeamAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<TeamResponseModel>(commandResult.Data)
                : new TeamResponseModel();

            return new TeamResultModel(readModel, commandResult.ValidationResult);
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpDelete("{teamId}")]
        public async Task<DeleteTeamResultModel> Delete(int teamId)
        {
            var command = new BaseTeamCommandModel 
            { 
                Id = teamId,
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
            };

            var commandResult = await _teamService.DeleteTeamAsync(command);

            var readModel = new DeleteTeamResponseModel
            {
                IsSuccessful = commandResult.IsValid
            };

            return new DeleteTeamResultModel(readModel, commandResult.ValidationResult);
        }

        [Route("~/api/[controller]/{teamId}/users")]
        [HttpGet]
        public async Task<UserTeamListResultModel> GetUsersInTeam(int teamId)
        {
            var command = new BaseUserTeamCommandModel
            {
                TeamId = teamId,
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
            };

            var commandResult = await _teamService.GetUsersInTeamAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<List<UserResponseModel>>(commandResult.Data)
                : new List<UserResponseModel>();

            return new UserTeamListResultModel(readModel, commandResult.ValidationResult);
        }

        [Route("~/api/[controller]/user/insert")]
        [HttpPost]
        public async Task<UserTeamResultModel> InsertUserToTeam([FromBody] UserTeamRequestModel formModel)
        {
            var command = CreateCommand<InsertUserTeamCommandModel, UserTeamRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _teamService.InsertUserToTeamAsync(command);

            var readModel = new UserTeamResponseModel
            {
                IsSuccessful = commandResult.IsValid
            };

            return new UserTeamResultModel(readModel, commandResult.ValidationResult);
        }

        [Route("~/api/[controller]/user/remove")]
        [HttpDelete]
        public async Task<UserTeamResultModel> RemoveUserToTeam([FromBody] UserTeamRequestModel formModel)
        {
            var command = CreateCommand<UserTeamCommandModel, UserTeamRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _teamService.RemoveUserFromTeamAsync(command);

            var readModel = new UserTeamResponseModel
            {
                IsSuccessful = commandResult.IsValid
            };

            return new UserTeamResultModel(readModel, commandResult.ValidationResult);
        }
    }
}
