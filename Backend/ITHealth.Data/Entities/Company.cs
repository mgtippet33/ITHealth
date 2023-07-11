namespace ITHealth.Data.Entities
{
    public class Company: BaseEntity
    {
        public string Name { get; set; } = null!;
        public string InviteCode { get; set; } = null!;

        public HashSet<User> Users { get; set; } = new HashSet<User>();
        public HashSet<Team> Teams { get; set; } = new HashSet<Team>();
        public HashSet<SubscribeHistory> SubscribeHistories { get; set; } = new HashSet<SubscribeHistory>();
    }
}
