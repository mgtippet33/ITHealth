using ITHealth.Data.Enums;

namespace ITHealth.Web.API.Models.Account.Update
{
    public class UpdateUserRequestModel
    {
        public string Email { get; set; } = null!;

        public string? Password { get; set; }

        public string FullName { get; set; } = null!;

        public Gender Gender { get; set; }

        public BloodType? BloodType { get; set; }

        public double AveragePressure { get; set; }

        public int WorkHoursCount { get; set; }
    }
}
