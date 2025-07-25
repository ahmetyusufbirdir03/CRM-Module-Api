using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id);

        // Relations

        // Customer ile bire çok ilişki (bir müşteri birçok görüşmeye sahip)
        builder.HasOne(m => m.Customer)
            .WithMany()
            .HasForeignKey(m => m.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // User ile bire çok ilişki (bir kullanıcı birçok görüşmeye sahip)
        builder.HasOne(m => m.User)
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // MeetingType ile bire çok ilişki
        builder.HasOne(m => m.MeetingType)
            .WithMany()
            .HasForeignKey(m => m.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // MeetingFormat ile bire çok ilişki
        builder.HasOne(m => m.MeetingFormat)
            .WithMany()
            .HasForeignKey(m => m.FormatId)
            .OnDelete(DeleteBehavior.Restrict);

        // MeetingState ile bire çok ilişki
        builder.HasOne(m => m.MeetingState)
            .WithMany()
            .HasForeignKey(m => m.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        // MeetingContent ile bire çok ilişki
        builder.HasOne(m => m.MeetingContent)
            .WithMany()
            .HasForeignKey(m => m.ContentId)
            .OnDelete(DeleteBehavior.Restrict);

        // MeetingParticipation koleksiyonu (bire çok)
        builder.HasMany(m => m.MeetingParticipations)
            .WithOne(mp => mp.Meeting)
            .HasForeignKey(mp => mp.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tarih alanları için opsiyonel ayarlar
        builder.Property(m => m.StartDate)
            .IsRequired();

        builder.Property(m => m.EndDate)
            .IsRequired();

        Faker faker = new();

    }
}
