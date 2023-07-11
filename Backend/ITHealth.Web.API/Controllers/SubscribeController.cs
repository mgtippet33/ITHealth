using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Subscribe;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Subscribe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITHealth.Web.API.Controllers
{
    [Localization]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscribeController : BaseApiController
    {
        private readonly ISubscribeService _subscribeService;

        public SubscribeController(ISubscribeService subscribeService, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(httpContextAccessor, mapper)
        {
            _subscribeService = subscribeService;
        }

        [Authorize(Roles = "GlobalAdministrator")]
        [HttpGet]
        public async Task<SubscribeResultModel> GetSubscribe()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

            var commandResult = await _subscribeService.GetLastSubscribeAsync(userEmail);

            var readModel = commandResult.IsValid
                ? _mapper.Map<SubscribeResponseModel>(commandResult.Data)
                : new SubscribeResponseModel();

            return new SubscribeResultModel(readModel, commandResult.ValidationResult);
        }

        [HttpPost]
        public async Task<SaveSubscribeResultModel> Payment([FromForm] LiqPayRequestModel formModel)
        {
            var command = CreateCommand<SaveSubscribeCommandModel, LiqPayRequestModel>(formModel);

            var commandResult = await _subscribeService.SaveSubscribeAsync(command);

            return new SaveSubscribeResultModel(new SaveSubscribeResponseModel() { IsSuccessful = commandResult.Data.IsSuccessful }, commandResult.ValidationResult);
        }
    }
}
