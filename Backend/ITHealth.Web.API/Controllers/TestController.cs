using System.Security.Claims;
using AutoMapper;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Web.API.Infrastructure.Filters;
using ITHealth.Web.API.Models.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITHealth.Web.API.Controllers;

[Localization]
[Route("api/[controller]/[action]")]
[Authorize]
[ApiController]
public class TestController : BaseApiController
{
    private readonly ITestService _testService;

    public TestController(IHttpContextAccessor httpContextAccessor, IMapper mapper, ITestService testService) : base(httpContextAccessor, mapper)
    {
        _testService = testService;
    }
    
    [HttpGet("{testId}")]
    public async Task<TestResultModel> Get(int testId)
    {
        var commandResult = await _testService.GetTestByIdAsync(testId);

        var readModel = _mapper.Map<TestResponseModel>(commandResult.Data);
    
        return new TestResultModel(readModel, commandResult.ValidationResult);
    }
    
    [HttpGet]
    public async Task<TestListResultModel> Get([FromQuery] TestPaginationRequestModel formModel)
    {
        var command = CreateCommand<GetTestsCommandModel, TestPaginationRequestModel>(formModel);
        command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

        var commandResult = await _testService.GetTestAsync(command);

        var readModel = _mapper.Map<TestPaginationResponseModel<TestResponseModel>>(commandResult.Data);
        
        return new TestListResultModel(readModel, commandResult.ValidationResult);
    }

    [HttpGet]
    public async Task<UserTestListResultModel> GetUserTests([FromQuery] TestPaginationRequestModel formModel)
    {
        var command = CreateCommand<GetUserTestListCommandModel, TestPaginationRequestModel>(formModel);
        command.UserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

        var commandResult = await _testService.GetUserTestsAsync(command);

        var readModel = _mapper.Map<TestPaginationResponseModel<UserTestInfoResponseModel>>(commandResult.Data);

        return new UserTestListResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [Route("~/api/[controller]/GetUserAnswers/test/{testId}/user/{userId}")]
    [HttpGet]
    public async Task<UserAnswersResultModel> GetUserAnswers(int testId, int userId)
    {
        var command = new GetUserAnswersCommandModel
        {
            TestId = testId,
            UserId = userId,
            CurrentEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!
        };

        var commandResult = await _testService.GetUserAnswersByTest(command);

        var readModel = _mapper.Map<UserAnswersResponseModel>(commandResult.Data);

        return new UserAnswersResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpGet]
    public async Task<TestPassedUserResultModel> GetTestPassedUsers([FromQuery] TestPassedUsersRequestModel formModel)
    {
        var command = CreateCommand<TestPassedUsersCommandModel, TestPassedUsersRequestModel>(formModel);

        var commandResult = await _testService.GetTestPassedUsersAsync(command);

        var readModel = _mapper.Map<List<TestPassedUserResponseModel>>(commandResult.Data);

        return new TestPassedUserResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpGet]
    public async Task<TestPassedListResultModel> GetTestsPassedUserMoreThreeTime([FromQuery] int userId)
    {
        var commandResult = await _testService.GetTestsPassedUserMoreThreeTimeAsync(userId);

        var readModel = _mapper.Map<List<TestPassedInfoResponseModel>>(commandResult.Data);

        return new TestPassedListResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpGet]
    public async Task<TestingStatisticsResultModel> GetUserTestingStatistics([FromQuery] UserTestingStatisticsRequestModel formModel)
    {
        var command = CreateCommand<UserTestingStatisticsCommandModel, UserTestingStatisticsRequestModel>(formModel);
        var commandResult = await _testService.GetUserTestingStatisticsByTestAsync(command);

        var readModel = _mapper.Map<List<UserTestingStatisticResponseModel>>(commandResult.Data);

        return new TestingStatisticsResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpGet]
    public async Task<UnverifiedUserTestsResultModel> GetUnverifiedTests([FromQuery] TestPaginationRequestModel formModel)
    {
        var command = CreateCommand<GetTestsCommandModel, TestPaginationRequestModel>(formModel);
        command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

        var commandResult = await _testService.GetUnverifiedTestsAsync(command);

        var readModel = _mapper.Map<TestPaginationResponseModel<UnverifiedUserTestResponseModel>>(commandResult.Data);

        return new UnverifiedUserTestsResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPost]
    public async Task<TestResultModel> Create([FromBody] TestRequestModel formModel)
    {
        var command = CreateCommand<CreateTestCommandModel, TestRequestModel>(formModel);
        command.CurrentUserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

        var commandResult = await _testService.CreateTestAsync(command);

        var readModel = commandResult.IsValid ?
            _mapper.Map<TestResponseModel>(commandResult.Data) :
            new TestResponseModel();

        return new TestResultModel(readModel, commandResult.ValidationResult);
    }
    
    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPut]
    public async Task<TestResultModel> Update([FromBody] UpdateTestRequestModel formModel)
    {
        var command = CreateCommand<UpdateTestCommandModel, UpdateTestRequestModel>(formModel);

        var commandResult = await _testService.UpdateTestAsync(command);

        var readModel = commandResult.IsValid ?
            _mapper.Map<TestResponseModel>(commandResult.Data) :
            new TestResponseModel();

        return new TestResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpDelete("{testId}")]
    public async Task<DeleteTestResultModel> Delete(int testId)
    {
        var command = new BaseTestCommandModel 
        { 
            Id = testId,
        };

        var commandResult = await _testService.DeleteTestAsync(command);

        var readModel = new DeleteTestResponseModel
        {
            IsSuccessful = commandResult.IsValid
        };

        return new DeleteTestResultModel(readModel, commandResult.ValidationResult);
    }
    
    [HttpPost]
    public async Task<TestResultsResultModel> SendForVerification([FromBody] SendRequestModel formModel)
    {
        var command = CreateCommand<SendTestCommandModel, SendRequestModel>(formModel);
        command.UserEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value!;

        var commandResult = await _testService.SendTestAsync(command);

        var readModel = commandResult.IsValid ?
            _mapper.Map<TestResultResponseModel>(commandResult.Data) :
            new TestResultResponseModel();

        return new TestResultsResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPost]
    public async Task<SendOpenUserAnswersAssesmentResultModel> SendOpenUserAnswersAssesment([FromBody] SendOpenUserAnswersAssesmentRequestModel formModel)
    {
        var command = CreateCommand<SendOpenUserAnswersAssesmentCommandModel, SendOpenUserAnswersAssesmentRequestModel>(formModel);

        var commandResult = await _testService.SendOpenUserAnswersAssesmentAsync(command);

        var readModel = new SendOpenUserAnswersAssesmentResponseModel
        {
            IsSuccessful = commandResult.IsValid
        };

        return new SendOpenUserAnswersAssesmentResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPost]
    public async Task<TestDeadlineResultModel> SetUsersTestDeadlines([FromBody] UsersTestDeadlineRequestModel formModel)
    {
        var command = CreateCommand<CreateUsersTestDeadlineCommandModel, UsersTestDeadlineRequestModel>(formModel);

        var commandResult = await _testService.SetTestDeadlinesAsync(command);

        var readModel = new TestDeadlineResponseModel
        {
            IsSuccessful = commandResult.IsValid
        };

        return new TestDeadlineResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPost]
    public async Task<TestDeadlineResultModel> SetTeamTestDeadlines([FromBody] TeamTestDeadlineRequestModel formModel)
    {
        var command = CreateCommand<CreateTeamTestDeadlineCommandModel, TeamTestDeadlineRequestModel>(formModel);

        var commandResult = await _testService.SetTestDeadlinesForTeamAsync(command);

        var readModel = new TestDeadlineResponseModel
        {
            IsSuccessful = commandResult.IsValid
        };

        return new TestDeadlineResultModel(readModel, commandResult.ValidationResult);
    }

    [Authorize(Roles = "Administrator,GlobalAdministrator")]
    [HttpPost]
    public async Task<TestingStatisticsResultModel> UsersTestingStatistics([FromBody] UsersTestingStatisticsRequestModel formModel)
    {
        var command = CreateCommand<UsersTestingStatisticsCommandModel, UsersTestingStatisticsRequestModel>(formModel);

        var commandResult = await _testService.GetUsersTestingStatisticsAsync(command);

        var readModel = _mapper.Map<List<UserTestingStatisticResponseModel>>(commandResult.Data);

        return new TestingStatisticsResultModel(readModel, commandResult.ValidationResult);
    }
}