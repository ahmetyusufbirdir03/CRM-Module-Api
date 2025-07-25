using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MeetingTypeConfiguration : IEntityTypeConfiguration<MeetingType>
{
    public void Configure(EntityTypeBuilder<MeetingType> builder)
    {
        builder.ToTable("MeetingTypes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Type)
            .IsRequired()
            .HasMaxLength(20); 
    }
}
