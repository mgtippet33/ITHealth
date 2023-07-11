using ITHealth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITHealth.Data.Configurations;

public class SubquestionConfiguration: IEntityTypeConfiguration<Subquestion>
{
    public void Configure(EntityTypeBuilder<Subquestion> builder)
    {
        builder.ToTable("Subquestions");
    }
}