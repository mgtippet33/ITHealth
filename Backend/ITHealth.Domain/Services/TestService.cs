using AutoMapper;
using ITHealth.Data;
using ITHealth.Data.Entities;
using ITHealth.Data.Enums;
using ITHealth.Domain.Contracts.Commands.Test;
using ITHealth.Domain.Contracts.Interfaces;
using ITHealth.Domain.Resources.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITHealth.Domain.Services;

public class TestService : BaseApplicationService, ITestService
{
    public TestService(UserManager<User> userManager, AppDbContext appDbContext, IServiceProvider serviceProvider,
        IMapper mapper) : base(userManager, appDbContext, serviceProvider, mapper)
    {
    }

    public async Task<TestCommandModelResult> GetTestByIdAsync(int testId)
    {
        var test = await _appDbContext.Tests
            .Include(e => e.Questions)
            .ThenInclude(q => q.Answers)
            .Include(e => e.Questions)
            .ThenInclude(a => a.Subquestions)
            .Include(e => e.TestResults)
            .Include(e => e.TestDeadlines)
            .Where(e => e.Id == testId)
            .SingleOrDefaultAsync();
        var testCommand = _mapper.Map<TestCommandModel>(test);

        return new TestCommandModelResult(testCommand);
    }

    public async Task<TestListCommandModelResult> GetTestAsync(GetTestsCommandModel command)
    {
        var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);

        var tests = await _appDbContext.Tests
            .Include(e => e.Questions)
            .ThenInclude(q => q.Answers)
            .Include(e => e.Questions)
            .ThenInclude(a => a.Subquestions)
            .Include(e => e.TestResults)
            .Include(e => e.TestDeadlines)
            .Where(x => x.CompanyId == user.CompanyId)
            .ToListAsync();

        var testsCommand = _mapper.Map<List<TestCommandModel>>(tests);

        var responseCommand = command.TestCount != 0
            ? GetTestsGroupByPages(command, testsCommand)
            : new TestPaginationResponseCommandModel<TestCommandModel>
            {
                CurrentPageNumber = 1,
                LastPageNumber = 1,
                Tests = testsCommand
            };

        return new TestListCommandModelResult(responseCommand);
    }

    public async Task<UserTestListCommandModelResult> GetUserTestsAsync(GetUserTestListCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);
        var resultCommand = new TestPaginationResponseCommandModel<UserTestInfoCommandModel>();

        if (validationResult != null && validationResult.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(command.UserEmail);
            var tests = await _appDbContext.Tests.ToListAsync();
            var testDeadlines = await _appDbContext.TestDeadlines.ToListAsync();

            var userTests = (from test in tests
                          join deadline in testDeadlines on test.Id equals deadline.TestId
                          where test.IsActive
                            && deadline.UserId == user.Id
                            && deadline.Deadline >= DateTime.Today
                            && !IsTestPassed(deadline)
                             select new UserTestInfoCommandModel
                          {
                              Id = test.Id,
                              Name = test.Name,
                              Description = test.Description,
                              Deadline = deadline.Deadline,
                          })
                          .DistinctBy(x => x.Id)
                          .OrderBy(x => x.Id)
                          .ToList();

            resultCommand = GetTestsGroupByPages(command, userTests);
        }

        return new UserTestListCommandModelResult(resultCommand, validationResult);
    }

    public async Task<GetUserAnswersCommandModelResult> GetUserAnswersByTest(GetUserAnswersCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);
        var responseCommand = new UserAnswersResponseCommandModel();

        if (validationResult != null && validationResult.IsValid)
        {
            var user = await _appDbContext.Users.FirstAsync(u => u.Id == command.UserId);
            var deadline = await _appDbContext.TestDeadlines
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(x => x.TestId == command.TestId && x.UserId == user.Id);

            if (deadline == null || !IsTestPassed(deadline))
            {
                responseCommand.UserId = user.Id;
                responseCommand.TestId = command.TestId;

                return new GetUserAnswersCommandModelResult(responseCommand, validationResult);
            }

            var questions = await _appDbContext.Questions
                .Include(e => e.Answers)
                .ThenInclude(e => e.UserAnswers.Where(answer => answer.UserId == user.Id))
                .Include(e => e.Subquestions)
                .ThenInclude(e => e.UserOpenAnswers.Where(answer => answer.UserId == user.Id))
                .Where(x => x.TestId == command.TestId)
                .ToListAsync();

            foreach (var question in questions)
            {
                responseCommand.Questions.Add(new QuestionUserAnswersResponseCommandModel
                {
                    QuestionId = question.Id,
                    QuestionDescription = question.Description,
                    Answers = await GetAnswersByQuestionAsync(user.Id, question),
                    UserOpenAnswer = await GetLastSubquestionsUserAnswersListAsync(user.Id, question.Id)
                });
            }

            responseCommand.UserId = user.Id;
            responseCommand.TestId  = command.TestId;
        }

        return new GetUserAnswersCommandModelResult(responseCommand, validationResult);
    }

    public async Task<TestPassedUsersCommandModelResult> GetTestPassedUsersAsync(TestPassedUsersCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);
        var responseCommand = new List<TestPassedUserResponseCommandModel>();

        if (validationResult != null && validationResult.IsValid)
        {
            var team = await _appDbContext.Teams
                .Include(u => u.Users)
                .SingleAsync(x => x.Id == command.TeamId);
            var teamUsers = team.Users.ToList();

            var testResults = GetLastUsersTestResult(teamUsers, command.TestId, command.StartDate, command.EndDate);

            responseCommand = testResults.AsEnumerable()
                .Select(x => new TestPassedUserResponseCommandModel
                {
                    UserId = x.UserId,
                    FullName = teamUsers.Single(u => u.Id == x.UserId).FullName,
                    PassingDate = x.CreatedAt.Date
                })
                .ToList();
        }

        return new TestPassedUsersCommandModelResult(responseCommand, validationResult);
    }

    public async Task<TestingStatisticsCommandModelResult> GetUsersTestingStatisticsAsync(UsersTestingStatisticsCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);
        var responseCommand = new List<UserTestingStatisticResponseCommandModel>();

        if (validationResult != null && validationResult.IsValid)
        {
            var users = await _userManager.Users.Where(u => command.UserIds.Contains(u.Id)).ToListAsync();

            var testResults = GetLastUsersTestResult(users, command.TestId, command.StartDate, command.EndDate);

            responseCommand = testResults.Select(x => new UserTestingStatisticResponseCommandModel
            {
                UserId = x.UserId,
                FullName = users.Single(u => u.Id == x.UserId).FullName,
                PassingDate = x.CreatedAt.Date,
                AssessmentPercent = CalculateTestAssessmentPercent(x),
            }).ToList();
        }

        return new TestingStatisticsCommandModelResult(responseCommand, validationResult);
    }

    public async Task<TestPassedListCommandModelResult> GetTestsPassedUserMoreThreeTimeAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var validationResult = ValidateNullData(user, "User", CommonResource.User_DoesntExist);
        var testPassedList = new List<TestCommandModel>();

        if (validationResult != null && validationResult.IsValid)
        {
            var testIds = await _appDbContext.TestResults
                .Where(x => x.UserId == user.Id)
                .GroupBy(x => x.TestId)
                .Where(x => x.Count() >= 3)
                .Select(x => x.Key)
                .ToListAsync();

            var tests = await _appDbContext.Tests.Where(x => testIds.Contains(x.Id)).ToListAsync();
            testPassedList = _mapper.Map<List<TestCommandModel>>(tests);
        }

        return new TestPassedListCommandModelResult(testPassedList, validationResult);
    }

    public async Task<TestingStatisticsCommandModelResult> GetUserTestingStatisticsByTestAsync(UserTestingStatisticsCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);
        var responseCommand = new List<UserTestingStatisticResponseCommandModel>();

        if (validationResult != null && validationResult.IsValid)
        {
            var testResults = await _appDbContext.TestResults
                .Where(x => x.UserId == command.UserId && x.TestId == command.TestId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            responseCommand = testResults.Select(x => new UserTestingStatisticResponseCommandModel
            {
                UserId = x.UserId,
                FullName = string.Empty,
                PassingDate = x.CreatedAt.Date,
                AssessmentPercent = CalculateTestAssessmentPercent(x),
            }).ToList();
        }

        return new TestingStatisticsCommandModelResult(responseCommand, validationResult);
    }

    public async Task<UnverifiedUserTestsCommandModelResult> GetUnverifiedTestsAsync(GetTestsCommandModel command)
    {
        var user = await _appDbContext.Users
            .Include(e => e.Teams)
            .ThenInclude(e => e.Users)
            .FirstOrDefaultAsync(x => x.Email == command.CurrentUserEmail);
        var validationResult = ValidateUserToken(user);
        var resultCommand = new TestPaginationResponseCommandModel<UnverifiedUserTestInfoCommandModel>();

        if (validationResult != null && validationResult.IsValid && user.CompanyId.HasValue)
        {
            var role = await GetUserRoleAsync(user);
            var users = new List<User>();

            if (role == Role.GlobalAdministrator.ToString())
            {
                var company = await _appDbContext.Companies
                    .Include(e => e.Users)
                    .FirstAsync(x => x.Id == user.CompanyId.Value);
                users = company.Users.ToList();
            }
            else
            {
                users = user.Teams.SelectMany(x => x.Users).DistinctBy(x => x.Id).ToList();
            }

            var tests = await GetLastUnverifiedUsersTest(users);

            resultCommand = GetTestsGroupByPages(command, tests);
        }

        return new UnverifiedUserTestsCommandModelResult(resultCommand, validationResult);
    }

    public async Task<TestCommandModelResult> CreateTestAsync(CreateTestCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(command.CurrentUserEmail);
            var entity = _mapper.Map<Test>(command);

            entity.TestResults = null;
            entity.TestDeadlines = null;
            entity.CompanyId = user.CompanyId.Value;
            entity.Company = await _appDbContext.Companies.SingleAsync(x => x.Id == user.CompanyId);

            await _appDbContext.Tests.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();

            command = _mapper.Map<CreateTestCommandModel>(entity);
        }

        return new TestCommandModelResult(command, validationResult);
    }

    public async Task<TestCommandModelResult> UpdateTestAsync(UpdateTestCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var test = await _appDbContext.Tests
                .AsNoTracking()
                .Include(e => e.Questions)
                .ThenInclude(q => q.Answers)
                .Include(e => e.Questions)
                .ThenInclude(a => a.Subquestions)
                .Include(e => e.TestResults)
                .Include(e => e.TestDeadlines)
                .Where(e => e.Id == command.Id)
                .SingleOrDefaultAsync();
            var updatedTest = _mapper.Map(command, test);

            _appDbContext.Tests.Update(updatedTest);
            await _appDbContext.SaveChangesAsync();

            command.Id = updatedTest.Id;
        }

        return new TestCommandModelResult(command, validationResult);
    }

    public async Task<TestCommandModelResult> DeleteTestAsync(BaseTestCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var test = await _appDbContext.Tests.SingleAsync(x => x.Id == command.Id);

            _appDbContext.Tests.Remove(test);
            await _appDbContext.SaveChangesAsync();
        }

        return new TestCommandModelResult(validationResult: validationResult);
    }

    public async Task<TestResultCommandModelResult> SendTestAsync(SendTestCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        var testResultCommandModel = new TestResultCommandModel();
        if (validationResult != null && validationResult.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(command.UserEmail);
            
            foreach (var userAnswer in command.UserAnswers)
            {
                if (userAnswer.AnswerId != 0)
                {
                    await _appDbContext.UserAnswers.AddAsync(new UserAnswer()
                    {
                        UserId = user.Id,
                        AnswerId = userAnswer.AnswerId,
                    });
                }

                if (userAnswer.SubAnswers.Any())
                {
                    var userOpenAnswers = userAnswer.SubAnswers.Select(x => new UserOpenAnswer
                    {
                        UserId = user.Id,
                        SubquestionId = x.SubquestionId,
                        Text = x.Answer ?? string.Empty
                    }).ToList();

                    await _appDbContext.OpenUserAnswers.AddRangeAsync(userOpenAnswers);
                }
            }

            var userTestResult = await CalculateTestResultAsync(command.UserAnswers);
            var testResult = new TestResult()
            {
                TestId = command.TestId,
                UserId = user.Id,
                Result = userTestResult.Points,
                MaxResult = userTestResult.MaxPoints,
                IsVerified = false,
            };
            
            await _appDbContext.TestResults.AddAsync(testResult);
            await _appDbContext.SaveChangesAsync();

            testResultCommandModel = _mapper.Map<TestResult, TestResultCommandModel>(testResult);
        }

        return new TestResultCommandModelResult(testResultCommandModel, validationResult);
    }

    public async Task<SendOpenUserAnswersAssesmentCommandModelResult> SendOpenUserAnswersAssesmentAsync(SendOpenUserAnswersAssesmentCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var testResults = await _appDbContext.TestResults
                .Where(x => x.UserId == command.UserId && x.TestId == command.TestId && !x.IsVerified)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            
            foreach (var result in testResults)
            {
                result.IsVerified = true;
            }

            var testResult = testResults.First();
            testResult.MaxResult += command.AdditionalScores.Sum();
            testResult.Result += command.AdditionalScores.Sum();

            await _appDbContext.SaveChangesAsync();
        }

        return new SendOpenUserAnswersAssesmentCommandModelResult(validationResult);
    }

    public async Task<UsersTestDeadlineCommandModelResult> SetTestDeadlinesAsync(CreateUsersTestDeadlineCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var test = await _appDbContext.Tests.FirstAsync(x => x.Id == command.TestId);
            var deadlines = new List<TestDeadline>();

            foreach (var userId in command.UserIds)
            {
                deadlines.Add(new TestDeadline
                {
                    UserId = userId,
                    TestId = test.Id,
                    Deadline = command.Deadline,
                    User = await _userManager.FindByIdAsync(userId.ToString()),
                    Test = test
                });
            }

            await _appDbContext.TestDeadlines.AddRangeAsync(deadlines);
            await _appDbContext.SaveChangesAsync();
        }

        return new UsersTestDeadlineCommandModelResult(command, validationResult);
    }

    public async Task<TeamTestDeadlineCommandModelResult> SetTestDeadlinesForTeamAsync(CreateTeamTestDeadlineCommandModel command)
    {
        var validationResult = await ValidateCommandAsync(command);

        if (validationResult != null && validationResult.IsValid)
        {
            var team = await _appDbContext.Teams.Include(x => x.Users).SingleAsync(x => x.Id == command.TeamId);
            var test = await _appDbContext.Tests.SingleAsync(x => x.Id == command.TestId);
            var deadlines = team.Users.Select(user => new TestDeadline
            {
                UserId = user.Id,
                TestId = command.TestId,
                Deadline = command.Deadline,
                User = user,
                Test = test
            }).ToList();

            await _appDbContext.TestDeadlines.AddRangeAsync(deadlines);
            await _appDbContext.SaveChangesAsync();
        }

        return new TeamTestDeadlineCommandModelResult(command, validationResult);
    }

    public async Task<bool> HasUserBadTestResultsAsync(int userId)
    {
        const double MinimumAvarageTestAssessmentPercent = 50.0;

        var testResults = await _appDbContext.TestResults
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync();

        var testAssessments = testResults.Select(x => CalculateTestAssessmentPercent(x)).ToList();
        var avarageTestAssessment = testAssessments.Any() ? testAssessments.Average() : 0;

        return avarageTestAssessment < MinimumAvarageTestAssessmentPercent;
    }

    private async Task<(int MaxPoints, int Points)> CalculateTestResultAsync(List<UserAnswerCommandModel> userAnswers)
    {
        var maxPoints = 0;
        var points = 0;

        foreach (var userAnswer in userAnswers)
        {
            if (userAnswer.AnswerId != 0)
            {
                var answer = await _appDbContext.Answers.SingleAsync(a => a.Id == userAnswer.AnswerId);
                var maxQuestionPoint = await _appDbContext.Answers.Where(x => x.QuestionId == userAnswer.QuestionId).MaxAsync(x => x.Weight);

                maxPoints += maxQuestionPoint;
                points += answer.Weight;
            }
        }

        return (maxPoints, points);
    }

    private bool IsTestPassed(TestDeadline deadline)
    {
        var testResult = _appDbContext.TestResults
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefault(x => x.UserId == deadline.UserId && x.TestId == deadline.TestId);

        return testResult != null && testResult.CreatedAt >= deadline.CreatedAt && testResult.CreatedAt <= deadline.Deadline;
    }

    private async Task<string?> GetLastSubquestionsUserAnswersListAsync(int userId, int questionId)
    {
        string? userOpenAnswer = null;

        var subquestion = await _appDbContext.Subquestions
            .Where(x => x.QuestionId == questionId)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        if (subquestion != null)
        {
            userOpenAnswer = await _appDbContext.OpenUserAnswers
                .Where(x => x.UserId == userId && x.SubquestionId == subquestion.Id)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.Text)
                .FirstOrDefaultAsync();
        }

        return userOpenAnswer;
    }

    private int CalculateTestAssessmentPercent(TestResult result)
    {
        return Convert.ToInt32(Convert.ToDecimal(result.Result) / Convert.ToDecimal(result.MaxResult) * 100);
    }

    private IEnumerable<TestResult> GetLastUsersTestResult(List<User> users, int testId, DateTime startDate, DateTime endDate)
    {
        var lastResults = (from testResult in _appDbContext.TestResults.AsEnumerable()
                          where users.Any(u => u.Id == testResult.UserId)
                            && testResult.TestId == testId
                            && testResult.CreatedAt.Date >= startDate
                            && testResult.CreatedAt.Date <= endDate
                          group testResult by testResult.UserId into result
                           select new
                           {
                               UserId = result.Key,
                               TestId = testId,
                               CreatedAt = result.Max(x => x.CreatedAt)
                           }).ToList();

        var testResults = (from testResult in _appDbContext.TestResults.AsEnumerable()
                          join lastResult in lastResults
                          on new { testResult.UserId, testResult.TestId, testResult.CreatedAt } equals new { lastResult.UserId, lastResult.TestId, lastResult.CreatedAt }
                          select new TestResult
                          {
                              UserId = testResult.UserId,
                              TestId = testResult.TestId,
                              Result = testResult.Result,
                              MaxResult = testResult.MaxResult,
                              CreatedAt = testResult.CreatedAt,
                              IsVerified = testResult.IsVerified
                          })
                          .DistinctBy(x => x.UserId)
                          .ToList();

        return testResults;
    }

    private async Task<List<UnverifiedUserTestInfoCommandModel>> GetLastUnverifiedUsersTest(List<User> users)
    {
        var lastResults = (from testResult in _appDbContext.TestResults.AsEnumerable()
                           where users.Any(u => u.Id == testResult.UserId)
                                 && !testResult.IsVerified
                           group testResult by new { testResult.UserId, testResult.TestId } into result
                           select new
                           {
                               UserId = result.Key.UserId,
                               TestId = result.Key.TestId,
                               CreatedAt = result.Max(x => x.CreatedAt)
                           }).ToList();

        var testResults = (from testResult in _appDbContext.TestResults.AsEnumerable()
                           join lastResult in lastResults
                           on new { testResult.UserId, testResult.TestId, testResult.CreatedAt } equals new { lastResult.UserId, lastResult.TestId, lastResult.CreatedAt }
                           select new TestResult
                           {
                               UserId = testResult.UserId,
                               TestId = testResult.TestId,
                               Result = testResult.Result,
                               MaxResult = testResult.MaxResult,
                               CreatedAt = testResult.CreatedAt,
                               IsVerified = testResult.IsVerified
                           }).ToList();

        var resultList = new List<UnverifiedUserTestInfoCommandModel>();

        foreach (var testResult  in testResults)
        {
            var unverifiedUserTestModel = new UnverifiedUserTestInfoCommandModel
            {
                TestId = testResult.TestId,
                TestName = (await _appDbContext.Tests.FirstAsync(x => x.Id == testResult.TestId)).Name,
                UserId = testResult.UserId,
                UserEmail = (await _appDbContext.Users.FirstAsync(x => x.Id == testResult.UserId)).Email,
                IsNeededAssessment = await _appDbContext.Questions
                    .Include(e => e.Subquestions)
                    .Where(x => x.TestId == testResult.TestId)
                    .AnyAsync(x => x.Subquestions.Any())
            };

            resultList.Add(unverifiedUserTestModel);
        }

        return resultList;
    }

    private async Task<List<TestAnswerResponseCommandModel>?> GetAnswersByQuestionAsync(int userId, Question question)
    {
        List<TestAnswerResponseCommandModel>? answerResponseList = null;

        var questionAnswers = await _appDbContext.Answers.Where(x => x.QuestionId == question.Id).ToListAsync();
        var lastAnswer = await _appDbContext.UserAnswers
            .Include(e => e.Answer)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(x => x.UserId == userId && x.Answer.QuestionId == question.Id);
        
        if (lastAnswer != null && questionAnswers.Any())
        {
            answerResponseList = _mapper.Map<List<TestAnswerResponseCommandModel>>(questionAnswers);
            answerResponseList.First(x => x.AnswerId == lastAnswer.AnswerId).IsChoosen = true;
        }

        return answerResponseList;
    }

    private TestPaginationResponseCommandModel<TInfoCommandModel> GetTestsGroupByPages<TCommandModel, TInfoCommandModel>(TCommandModel command, List<TInfoCommandModel> testList)
        where TCommandModel : TestPaginationRequestCommandModel
    {
        var lastPageNumber = Convert.ToDecimal(testList.Count) / Convert.ToDecimal(command.TestCount);
        var testGroupByPage = testList.Select((x, i) => new { Index = i, Value = x })
                                       .GroupBy(x => x.Index / command.TestCount)
                                       .Select(x => x.Select(x => x.Value).ToList())
                                       .ToList();

        return new TestPaginationResponseCommandModel<TInfoCommandModel>
        {
            CurrentPageNumber = command.CurrentPageNumber,
            LastPageNumber = Convert.ToInt32(Math.Ceiling(lastPageNumber)),
            Tests = testGroupByPage != null && testGroupByPage.Count != 0
                  ? testGroupByPage[command.CurrentPageNumber - 1]
                  : new List<TInfoCommandModel>()
        };
    }
}