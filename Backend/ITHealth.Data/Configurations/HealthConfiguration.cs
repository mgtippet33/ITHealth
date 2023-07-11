using ITHealth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHealth.Data.Configurations;

public class HealthConfiguration : IEntityTypeConfiguration<Health>
{
    public void Configure(EntityTypeBuilder<Health> builder)
    {
        builder.ToTable("Healths");
    }
}