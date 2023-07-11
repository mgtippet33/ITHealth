using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands.Health
{
    public class ListStressLevelCommandModelResult : BaseCommandModelResult<ListStressLevelCommandModel>
    {
        public ListStressLevelCommandModelResult(ListStressLevelCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class ListStressLevelCommandModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<StressLevelCommandModel> StressLevels { get; set; } = new();
    }

    public class StressLevelCommandModel
    {
        public DateTime Date { get; set; }

        public double StressLevel { get; set; }
    }
}
