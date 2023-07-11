using ITHealth.Domain.Contracts.Commands.Test;

namespace ITHealth.Domain.Contracts.Interfaces;

public interface ITestService
{
    Task<TestCommandModelResult> GetTestByIdAsync(int testId);

    Task<TestListCommandModelResult> GetTestAsync(GetTestsCommandModel command);

    Task<UserTestListCommandModelResult> GetUserTestsAsync(GetUserTestListCommandModel command);

    Task<GetUserAnswersCommandModelResult> GetUserAnswersByTest(GetUserAnswersCommandModel command);

    Task<TestPassedUsersCommandModelResult> GetTestPassedUsersAsync(TestPassedUsersCommandModel command);

    Task<TestingStatisticsCommandModelResult> GetUsersTestingStatisticsAsync(UsersTestingStatisticsCommandModel command);

    Task<TestPassedListCommandModelResult> GetTestsPassedUserMoreThreeTimeAsync(int userId);

    Task<TestingStatisticsCommandModelResult> GetUserTestingStatisticsByTestAsync(UserTestingStatisticsCommandModel command);

    Task<UnverifiedUserTestsCommandModelResult> GetUnverifiedTestsAsync(GetTestsCommandModel command);

    Task<TestCommandModelResult> CreateTestAsync(CreateTestCommandModel command);

    Task<TestCommandModelResult> UpdateTestAsync(UpdateTestCommandModel command);

    Task<TestCommandModelResult> DeleteTestAsync(BaseTestCommandModel command);
    
    Task<TestResultCommandModelResult> SendTestAsync(SendTestCommandModel command);

    Task<SendOpenUserAnswersAssesmentCommandModelResult> SendOpenUserAnswersAssesmentAsync(SendOpenUserAnswersAssesmentCommandModel command);

    Task<UsersTestDeadlineCommandModelResult> SetTestDeadlinesAsync(CreateUsersTestDeadlineCommandModel command);

    Task<TeamTestDeadlineCommandModelResult> SetTestDeadlinesForTeamAsync(CreateTeamTestDeadlineCommandModel command);

    Task<bool> HasUserBadTestResultsAsync(int userId);
}