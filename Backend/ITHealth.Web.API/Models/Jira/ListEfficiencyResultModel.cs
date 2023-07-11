using FluentValidation.Results;

namespace ITHealth.Web.API.Models.Jira
{
    public class ListEfficiencyResultModel : BaseOperationResultModel<ListEfficiencyResponseModel>
    {
        public ListEfficiencyResultModel(ListEfficiencyResponseModel data, ValidationResult validationResult) : base(data, validationResult)
        {
        }
    }

    public class ListEfficiencyResponseModel
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<EfficiencyResponseModel> Efficiencies { get; set; } = new();
    }

    public class EfficiencyResponseModel
    {
        public DateTime Date { get; set; }

        public double Efficiency { get; set; }
    }
}