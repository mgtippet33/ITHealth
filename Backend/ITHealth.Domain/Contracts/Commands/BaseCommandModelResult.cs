using FluentValidation.Results;

namespace ITHealth.Domain.Contracts.Commands
{
    public class BaseCommandModelResult<TData>
    {
        public TData Data { get; set; }

        public ValidationResult ValidationResult { get; private set; }

        public bool IsValid
        {
            get
            {
                return ValidationResult != null && ValidationResult.IsValid;
            }
        }

        public BaseCommandModelResult(TData data, ValidationResult validationResult)
        {
            Data = data;
            ValidationResult = validationResult;
        }
    }
}
