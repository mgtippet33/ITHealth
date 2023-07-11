using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Health;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Health;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ITHealth.Domain.Contracts.Commands.WorkingHours;
using ITHealth.Web.API.Models.WorkTime;

namespace ITHealth.Web.API.Controllers
{
    [Localization]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class HealthController : BaseApiController
    {
        private readonly IHealthService _healthService;
        private readonly IHoursService _hoursService;

        public HealthController(IHealthService healthService, IHttpContextAccessor httpContextAccessor, IMapper mapper,
            IHoursService hoursService) : base(httpContextAccessor, mapper)
        {
            _healthService = healthService;
            _hoursService = hoursService;
        }

        [HttpGet]
        public async Task<ListStressLevelResultModel> GetUserStressLevels(
            [FromQuery] GetUserStressLevelsRequestModel formModel)
        {
            var command = CreateCommand<GetUserStressLevelsCommandModel, GetUserStressLevelsRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _healthService.GetUserStressLevelsAsync(command);

            var readModel = _mapper.Map<ListStressLevelResponseModel>(commandResult.Data);

            return new ListStressLevelResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpPost]
        public async Task<HealthResultModel> Create([FromBody] HealthRequestModel formModel)
        {
            var command = CreateCommand<CreateHealthCommandModel, HealthRequestModel>(formModel);
            command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

            var commandResult = await _healthService.CreateHealthRecordAsync(command);

            var readModel = commandResult.IsValid
                ? _mapper.Map<HealthResponseModel>(commandResult.Data)
                : new HealthResponseModel();

            return new HealthResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpGet]
        public async Task<BurnoutResultModel> GetBurnoutInformation([FromQuery] string? email = null)
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _healthService.GetBurnoutInformationAsync(email ?? currentUserEmail);

            var readModel = commandResult.IsValid
                ? _mapper.Map<BurnoutResponseModel>(commandResult.Data)
                : new BurnoutResponseModel();

            return new BurnoutResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpGet]
        public async Task<GetWorkTimeStatisticsResultModel> GetUserWorkTime()
        {
            var currentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _hoursService.GetEfficiencyStatisticsAsync(
                new GetWorkingTimeStatisticsCommandModel()
                {
                    Email = currentUserEmail,
                });

            var readModel = commandResult != null
                ? _mapper.Map<GetWorkTimeStatisticsResponseModel>(commandResult.Data)
                : new GetWorkTimeStatisticsResponseModel();

            return new GetWorkTimeStatisticsResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpGet]
        public async Task<GetTeamWorkTimeStatisticsResultModel> GetTeamWorkTime([FromQuery] int? teamId = null)
        {
            var commandResult = await _hoursService.GetTeamEfficiencyStatisticsAsync(
                new GetTeamWorkingTimeStatisticsCommandModel()
                {
                    TeamId = teamId.Value
                });

            var readModel = commandResult != null
                ? _mapper.Map<GetTeamWorkTimeStatisticsResponseModel>(commandResult.Data)
                : new GetTeamWorkTimeStatisticsResponseModel();

            return new GetTeamWorkTimeStatisticsResultModel(readModel, commandResult.ValidationResult);
        }
    }
}