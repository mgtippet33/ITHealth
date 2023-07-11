using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Clockify.Key;
using ITHealth.Domain.Contracts.Commands.Clockify.Tracker;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Clockify.Secrets;
using ITHealth.Web.API.Models.Clockify.Tracker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITHealth.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Localization]
    public class ClockifyController : BaseApiController
    {
        private readonly IClockifyService _clockifyService;

        public ClockifyController(IMapper mapper, IClockifyService clockifyService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, mapper)
        {
            _clockifyService = clockifyService;
        }

        [Authorize(Roles = "Administrator,GlobalAdministrator")]
        [HttpPost]
        public async Task<ClockifySetSecretsResultModel> SetSecrets([FromBody] ClockifySetSecretsRequestModel formModel)
        {
            var command = CreateCommand<SetClockifySecretsCommandModel, ClockifySetSecretsRequestModel>(formModel);
            var commandResult = await _clockifyService.SetClockifySecretsAsync(command);

            if (!commandResult.IsValid)
                return new ClockifySetSecretsResultModel(
                    new ClockifySetSecretsResponseModel() { IsSuccessful = false }, commandResult.ValidationResult);

            return new ClockifySetSecretsResultModel(new ClockifySetSecretsResponseModel() { IsSuccessful = true });
        }

        [HttpGet]
        public async Task<ClockifyTimeEntriesResultModel> GetTimeEntries()
        {
            var command = new ClockifyTrackerCommandModel
            {
                CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value
            };
            var commandResult = await _clockifyService.GetUserTimeEntriesAsync(command);
            
            var readModel = commandResult.IsValid
                ? _mapper.Map<ClockifyTimeEntriesResponseModel>(commandResult.Data)
                : new ClockifyTimeEntriesResponseModel();

            return new ClockifyTimeEntriesResultModel(readModel, commandResult.ValidationResult);
        }
    }
}
