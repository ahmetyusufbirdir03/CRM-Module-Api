using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ForwardPlanConfiguration : IEntityTypeConfiguration<ForwardPlan>
{
    public void Configure(EntityTypeBuilder<ForwardPlan> builder)
    {
        builder.ToTable("ForwardPlans");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(c => c.Description)
            .IsRequired(true)
            .HasMaxLength(200);

    }
}
