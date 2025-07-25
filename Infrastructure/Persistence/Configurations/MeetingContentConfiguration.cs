using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MeetingContentConfiguration : IEntityTypeConfiguration<MeetingContent>
{
    public void Configure(EntityTypeBuilder<MeetingContent> builder)
    {
        builder.ToTable("MeetingContents");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Title)
            .IsRequired(true)
            .HasMaxLength(20);

        builder.Property(c => c.Content)
            .IsRequired(true)
            .HasMaxLength(200);

        builder.HasOne(mc => mc.ForwardPlan)
          .WithMany() 
          .HasForeignKey(mc => mc.ForwardPlanId)
          .OnDelete(DeleteBehavior.Restrict);

    }
}
