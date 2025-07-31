using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.ToTable("Meetings");

            builder.HasKey(m => m.Id);

            // Zorunlu alanlar
            builder.Property(m => m.StartDate)
                   .IsRequired();

            builder.Property(m => m.EndDate)
                   .IsRequired();

            builder.Property(m => m.Description)
                   .IsRequired()
                   .HasMaxLength(500);  // İstersen uzunluk sınırı koyabilirsin

            // İlişkiler

            // Customer ile 1-to-many ilişki
            builder.HasOne(m => m.Customer)
                   .WithMany()  // Eğer Customer içinde Meetings koleksiyonu yoksa boş bırakabilirsin
                   .HasForeignKey(m => m.CustomerId)
                   .OnDelete(DeleteBehavior.NoAction); 

            // User (AppUser) ile 1-to-many ilişki
            builder.HasOne(m => m.User)
                   .WithMany() // AppUser içinde Meetings koleksiyonu varsa ekleyebilirsin
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            // MeetingType ile ilişki
            builder.HasOne(m => m.MeetingType)
                   .WithMany()
                   .HasForeignKey(m => m.TypeId)
                   .OnDelete(DeleteBehavior.NoAction);

            // MeetingFormat ile ilişki
            builder.HasOne(m => m.MeetingFormat)
                   .WithMany()
                   .HasForeignKey(m => m.FormatId)
                   .OnDelete(DeleteBehavior.NoAction);

            // MeetingState ile ilişki
            builder.HasOne(m => m.MeetingState)
                   .WithMany()
                   .HasForeignKey(m => m.StateId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
