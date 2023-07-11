using ITHealth.Data.Enums;

namespace ITHealth.Domain.Contracts.Commands.Account.Update
{
    public class UpdateUserCommandModel : BaseCommandModel
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string? OldEmail { get; set; }

        public string? Password { get; set; }

        public string FullName { get; set; } = null!;

        public string? Role { get; set; }

        public Gender Gender { get; set; }

        public BloodType? BloodType { get; set; }

        public double AveragePressure { get; set; }

        public int WorkHoursCount { get; set; }

        public bool ConnectedToCompany { get; set; }
    }
}
