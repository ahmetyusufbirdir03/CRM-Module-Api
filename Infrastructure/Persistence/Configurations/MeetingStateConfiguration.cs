using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MeetingStateConfiguration : IEntityTypeConfiguration<MeetingState>
{
    public void Configure(EntityTypeBuilder<MeetingState> builder)
    {
        builder.ToTable("MeetingStates");
        
        builder.HasKey(t => t.Id);

        builder.Property(c => c.State)
            .IsRequired(true)
            .HasMaxLength(20);

        

    }
}
