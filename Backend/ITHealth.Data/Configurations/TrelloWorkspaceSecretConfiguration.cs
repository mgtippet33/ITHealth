using ITHealth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHealth.Data.Configurations;

public class TrelloWorkspaceSecretConfiguration : IEntityTypeConfiguration<TrelloWorkspaceSecrets>
{
    public void Configure(EntityTypeBuilder<TrelloWorkspaceSecrets> builder)
    {
        builder.HasKey(x => x.TeamId);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("getdate()")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.ToTable("TrelloWorkspaceSecrets");
    }
}