namespace ITHealth.Data.Entities
{
    public class SubscribeHistory : BaseEntity
    {
        public int CompanyId { get; set; }
        public double Price { get; set; }
        public DateTime EndDate { get; set; }

        public Company Company { get; set; } = new Company();
    }
}
