using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configurations;

public class MeetingFormatConfiguration : IEntityTypeConfiguration<MeetingFormat>
{
    public void Configure(EntityTypeBuilder<MeetingFormat> builder)
    {
        builder.ToTable("MeetingFormats");

        builder.HasKey(t => t.Id);

        builder.Property(c => c.Format)
            .IsRequired()
            .HasMaxLength(20);

        

    }
}
