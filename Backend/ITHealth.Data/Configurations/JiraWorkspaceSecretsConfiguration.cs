using ITHealth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHealth.Data.Configurations;

public class JiraWorkspaceSecretsConfiguration : IEntityTypeConfiguration<JiraWorkspaceSecrets>
{
    public void Configure(EntityTypeBuilder<JiraWorkspaceSecrets> builder)
    {
        builder.HasKey(x => x.TeamId);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.ToTable("JiraWorkspaceSecrets");
    }
}