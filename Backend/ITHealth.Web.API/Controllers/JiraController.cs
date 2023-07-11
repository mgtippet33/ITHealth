using System.Security.Claims;
using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Jira;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Jira;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITHealth.Web.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Localization]
[Authorize]
public class JiraController : BaseApiController
{
    private readonly IJiraService _jiraService;

    public JiraController(IJiraService jiraService, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(
        httpContextAccessor, mapper)
    {
        _jiraService = jiraService;
    }
    
    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPost]
    public async Task<JiraSetSecretsResultModel> SetSecrets([FromBody] JiraSetSecretsRequestModel formModel)
    {
        var command = CreateCommand<SetJiraSecretsCommandModel, JiraSetSecretsRequestModel>(formModel);
        var commandResult = await _jiraService.SetJiraSecretsAsync(command);

        if (!commandResult.IsValid)
            return new JiraSetSecretsResultModel(
                new JiraSetSecretsResponseModel() { IsSuccessful = false }, commandResult.ValidationResult);

        return new JiraSetSecretsResultModel(new JiraSetSecretsResponseModel() { IsSuccessful = true });
    }
    
    [HttpGet]
    public async Task<JiraCurrentUserTasksInProgressResultModel> GetCurrentUserTasksInProgress()
    {
        var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        var commandResult = await _jiraService.OpenedUserTaskAsync(email);

        var readModel = commandResult.IsValid
            ? _mapper.Map<JiraCurrentUserTasksInProgressResponseModel>(commandResult.Data)
            : new JiraCurrentUserTasksInProgressResponseModel();

        return new JiraCurrentUserTasksInProgressResultModel(readModel, commandResult.ValidationResult);
    }

    [HttpGet]
    public async Task<JiraCurrentUserTimeEntriesResultModel> GetTimeEntriesInSeconds()
    {
        var email = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
        var seconds = await _jiraService.UserTimeInSecondsAsync(email);

        return new JiraCurrentUserTimeEntriesResultModel(seconds);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpGet]
    public async Task<ListEfficiencyResultModel> GetEfficiencyStatistics([FromQuery] GetEfficiencyStatisticsRequestModel formModel)
    {
        var command = CreateCommand<GetUserEfficiencyCommandModel, GetEfficiencyStatisticsRequestModel>(formModel);

        var commandResult = await _jiraService.GetEfficiencyStatisticsAsync(command);

        var readModel = _mapper.Map<ListEfficiencyResponseModel>(commandResult.Data);

        return new ListEfficiencyResultModel(readModel, commandResult.ValidationResult);
    }
}