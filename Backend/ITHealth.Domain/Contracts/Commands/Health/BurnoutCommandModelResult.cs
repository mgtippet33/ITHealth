using FluentValidation.Results;
using ITHealth.Domain.Resources;

namespace ITHealth.Domain.Contracts.Commands.Health
{
    public class BurnoutCommandModelResult : BaseCommandModelResult<BurnoutResponseCommandModel>
    {
        public BurnoutCommandModelResult(BurnoutResponseCommandModel data = null, ValidationResult validationResult = null) : base(data, validationResult)
        {
        }
    }

    public class BurnoutResponseCommandModel
    {
        public bool HasStress { get; set; } = false;

        public bool HasBadSleep { get; set; } = false;

        public bool HasLowEfficiency { get; set; } = false;

        public bool HasBadTestResults { get; set; } = false;

        public bool HasOvertime { get; set; } = false;

        public string GeneralState
        {
            get
            {
                var states = new List<bool> { HasStress, HasOvertime, HasBadSleep, HasLowEfficiency, HasBadTestResults };
                var negativeStateCount = states.Count(x => x);

                switch (negativeStateCount)
                {
                    case 0:
                        return CommonResource.ExcellentState;
                    case 1:
                    case 2:
                        return CommonResource.GoodState;
                    case 3:
                        return CommonResource.NormalState;
                    case 4:
                        return CommonResource.BadState;
                    case 5:
                        return CommonResource.CriticalState;
                    default:
                        return CommonResource.UndeterminedState;
                }
            }
        }
    }
}
