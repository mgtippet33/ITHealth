namespace ITHealth.Data.Entities
{
    public class Health: BaseEntity
    {
        public int UserID { get; set; }
        public double Pressure { get; set; }
        public int Pulse { get; set; }
        public double Weight { get; set; }
        public double SleepTime { get; set; }

        public User User { get; set; } = new User();
    }
}
