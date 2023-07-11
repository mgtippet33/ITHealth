using AutoMapper;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITHealth.Web.API.Controllers;

[Localization]
[Route("api/[controller]/[action]")]
[Authorize]
[ApiController]
public class QuestionController : BaseApiController
{
    private readonly IQuestionService _questionService;

    public QuestionController(IHttpContextAccessor httpContextAccessor, IMapper mapper, IQuestionService questionService) : base(httpContextAccessor, mapper)
    {
        _questionService = questionService;
    }
    
    [HttpGet("{id}")]
    public async Task<QuestionResultModel> Get(int id)
    {
        var commandResult = await _questionService.GetQuestionByIdAsync(id);

        var readModel = _mapper.Map<QuestionResponseModel>(commandResult.Data);
    
        return new QuestionResultModel(readModel, commandResult.ValidationResult);
    }
    
    [HttpGet]
    public async Task<QuestionListResultModel> Get()
    {
        var commandResult = await _questionService.GetQuestionsAsync();

        var readModel = _mapper.Map<List<QuestionResponseModel>>(commandResult.Data);
        
        return new QuestionListResultModel(readModel, commandResult.ValidationResult);
    }
    
}