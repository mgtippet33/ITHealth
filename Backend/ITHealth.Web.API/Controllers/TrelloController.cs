using System.Security.Claims;
using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Trello.Key;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Trello;
using ITHealth.Web.API.Models.Trello.Key;
using ITHealth.Web.API.Models.Trello.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITHealth.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [Localization]
    public class TrelloController : BaseApiController
    {
        private readonly ITrelloService _trelloService;

        public TrelloController(IMapper mapper, ITrelloService trelloService, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor, mapper)
        {
            _trelloService = trelloService;
        }
        
        [HttpPost]
        public async Task<TrelloSetSecretsResultModel> SetSecrets([FromBody] TrelloSetSecretsRequestModel formModel)
        {
            var command = CreateCommand<SetAppKeyCommandModel, TrelloSetSecretsRequestModel>(formModel);
            var commandResult = await _trelloService.SetAppKeyAsync(command);

            if (!commandResult.IsValid)
                return new TrelloSetSecretsResultModel(
                    new TrelloSetKeyResponseModel { IsSuccessful = false }, commandResult.ValidationResult);

            return new TrelloSetSecretsResultModel(new TrelloSetKeyResponseModel { IsSuccessful = true });
        }
        
        [HttpGet]
        public async Task<TrelloGetSecretsResultModel> GetTeamTrelloSecret()
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _trelloService.GetSecretsAsync(email);

            return new TrelloGetSecretsResultModel(commandResult.FirstOrDefault());
        }

        [HttpGet]
        public async Task<GetCurrentUserTasksInProgressResultModel> GetCurrentUserTasksInProgress([FromQuery] TrelloGetCurrentTasksRequestModel request)
        {
            var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _trelloService.OpenedUserTaskAsync(request.Token, email);

            var readModel = commandResult.IsValid
                ? _mapper.Map<GetCurrentUserTasksInProgressResponseModel>(commandResult.Data)
                : new GetCurrentUserTasksInProgressResponseModel();

            return new GetCurrentUserTasksInProgressResultModel(readModel, commandResult.ValidationResult);
        }
    }
}