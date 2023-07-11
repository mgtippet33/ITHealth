using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Subquestion;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Subquestion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITHealth.Web.API.Controllers;

[Localization]
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class SubquestionController : BaseApiController
{
    private readonly ISubquestionService _subquestionService;

    public SubquestionController(IHttpContextAccessor httpContextAccessor, IMapper mapper, ISubquestionService subquestionService) : base(httpContextAccessor, mapper)
    {
        _subquestionService = subquestionService;
    }
    
    [HttpGet("{questionId}")]
    public async Task<SubquestionListResultModel> List(int questionId)
    {
        var commandResult = await _subquestionService.GetSubquestionByQuestionAsync(questionId);

        var readModel = _mapper.Map<List<SubquestionResponseModel>>(commandResult.Data);

        return new SubquestionListResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPut]
    public async Task<SubquestionResultModel> Update([FromBody] UpdateSubquestionRequestModel formModel)
    {
        var command = CreateCommand<UpdateSubquestionCommandModel, UpdateSubquestionRequestModel>(formModel);

        var commandResult = await _subquestionService.UpdateSubquestionAsync(command);

        var readModel = commandResult.IsValid ?
            _mapper.Map<SubquestionResponseModel>(commandResult.Data) :
            new SubquestionResponseModel();

        return new SubquestionResultModel(readModel, commandResult.ValidationResult);
    }
}