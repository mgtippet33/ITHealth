using FluentValidation.Results;

namespace ITHealth.Web.API.Models
{
    public class BaseOperationResultModel<TData>
    {
        public TData Data { get; set; }

        public IDictionary<string, IList<string>> Errors { get; private set; }

        public BaseOperationResultModel(TData data, ValidationResult validationResult)
        {
            Data = data;
            Errors = validationResult?.Errors != null ?
                GetValidationErrorsDictionary(validationResult.Errors) :
                new Dictionary<string, IList<string>>();
        }

        private IDictionary<string, IList<string>> GetValidationErrorsDictionary(IList<ValidationFailure> errors)
        {
            var result = new Dictionary<string, IList<string>>();

            foreach(var error in errors)
            {
                if (result.ContainsKey(error.PropertyName))
                {
                    result.GetValueOrDefault(error.PropertyName)?.Add(error.ErrorMessage);
                }
                else
                {
                    result.Add(error.PropertyName, new List<string> { error.ErrorMessage });
                }
            }

            return result;
        }
    }
}
