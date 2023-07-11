using ITHealth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHealth.Data.Configurations
{
    public class TestDeadlineConfiguration : IEntityTypeConfiguration<TestDeadline>
    {
        public void Configure(EntityTypeBuilder<TestDeadline> builder)
        {
            builder.ToTable("TestDeadlines");
        }
    }
}
