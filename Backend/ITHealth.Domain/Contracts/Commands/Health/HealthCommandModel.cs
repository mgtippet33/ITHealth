namespace ITHealth.Domain.Contracts.Commands.Health
{
    public class HealthCommandModel : BaseHealthCommandModel
    {
        public int UserId { get; set; }

        public double Pressure { get; set; }

        public int Pulse { get; set; }

        public double Weight { get; set; }

        public double SleepTime { get; set; }
    }
}
