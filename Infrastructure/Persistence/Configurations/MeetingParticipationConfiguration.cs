using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MeetingParticipationConfiguration : IEntityTypeConfiguration<MeetingParticipation>
{
    public void Configure(EntityTypeBuilder<MeetingParticipation> builder)
    {
        builder.ToTable("MeetingParticipations");

        builder.HasKey(mp => mp.Id);

        builder.Property(mp => mp.UserId)
               .IsRequired();

        builder.Property(mp => mp.MeetingId)
               .IsRequired();

        // Relationships

        builder.HasOne(mp => mp.AppUser)
               .WithMany(u => u.MeetingParticipations)
               .HasForeignKey(mp => mp.UserId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(mp => mp.Meeting)
               .WithMany(m => m.MeetingParticipations)
               .HasForeignKey(mp => mp.MeetingId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
