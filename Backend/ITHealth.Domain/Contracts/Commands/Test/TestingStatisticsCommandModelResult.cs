using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Test
{
    public class TestingStatisticsCommandModelResult : BaseCommandModelResult<List<UserTestingStatisticResponseCommandModel>>
    {
        public TestingStatisticsCommandModelResult(List<UserTestingStatisticResponseCommandModel> data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class UserTestingStatisticResponseCommandModel : TestPassedUserResponseCommandModel
    {
        public int AssessmentPercent { get; set; }
    }
}
