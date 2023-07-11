using ITHealth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHealth.Data.Configurations
{
    public class ClockifyWorkspaceSecretsConfiguration : IEntityTypeConfiguration<ClockifyWorkspaceSecrets>
    {
        public void Configure(EntityTypeBuilder<ClockifyWorkspaceSecrets> builder)
        {
            builder.HasKey(x => x.TeamId);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("getdate()")
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder.ToTable("ClockifyWorkspaceSecrets");
        }
    }
}
