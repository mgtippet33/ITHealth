using System.Security.Claims;
using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Answer;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Answer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITHealth.Web.API.Controllers;

[Localization]
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(Roles = "Administrator,GlobalAdministrator")]
public class AnswerController : BaseApiController
{
    private readonly IAnswerService _answerService;
    
    public AnswerController(IHttpContextAccessor httpContextAccessor, IMapper mapper, IAnswerService answerService) : base(httpContextAccessor,
        mapper)
    {
        _answerService = answerService;
    }

    [HttpGet("{questionId}")]
    public async Task<AnswerListResultModel> List(int questionId)
    {
        var commandResult = await _answerService.GetAnswersByQuestionAsync(questionId);

        var readModel = _mapper.Map<List<AnswerResponseModel>>(commandResult.Data);

        return new AnswerListResultModel(readModel, commandResult.ValidationResult);
    }
    
    [HttpPut]
    public async Task<AnswerResultModel> Update([FromBody] UpdateAnswerRequestModel formModel)
    {
        var command = CreateCommand<UpdateAnswerCommandModel, UpdateAnswerRequestModel>(formModel);

        var commandResult = await _answerService.UpdateAnswerAsync(command);

        var readModel = commandResult.IsValid ?
            _mapper.Map<AnswerResponseModel>(commandResult.Data) :
            new AnswerResponseModel();

        return new AnswerResultModel(readModel, commandResult.ValidationResult);
    }
    
    [HttpDelete("{answerId}")]
    public async Task<DeleteAnswerResultModel> Delete(int answerId)
    {
        var command = new BaseAnswerCommandModel()
        {
            Id = answerId,
        };

        var commandResult = await _answerService.DeleteAnswerAsync(command);

        var readModel = new DeleteAnswerResponseModel
        {
            IsSuccessful = commandResult.IsValid
        };

        return new DeleteAnswerResultModel(readModel, commandResult.ValidationResult);
    }
}