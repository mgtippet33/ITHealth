using ITHealth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHealth.Data.Configurations;

public class SubscribeHistoryConfiguration: IEntityTypeConfiguration<SubscribeHistory>
{
    public void Configure(EntityTypeBuilder<SubscribeHistory> builder)
    {
        builder.ToTable("SubscribeHistories");
    }
}