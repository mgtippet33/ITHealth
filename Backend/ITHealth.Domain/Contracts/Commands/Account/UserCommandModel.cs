using ITHealth.Data.Enums;

namespace ITHealth.Domain.Contracts.Commands.Account
{
    public class UserCommandModel : BaseCommandModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public Gender Gender { get; set; }

        public BloodType? BloodType { get; set; }

        public double AveragePressure { get; set; }

        public int WorkHoursCount { get; set; }

        public bool ConnectedToCompany { get; set; }
    }
}
