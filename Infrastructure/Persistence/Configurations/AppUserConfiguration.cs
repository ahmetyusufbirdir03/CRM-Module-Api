using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {

        builder.Property(u => u.FirstName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.LastName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.Address)
               .HasMaxLength(200);

        builder.Property(u => u.Department)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(u => u.Title)
               .IsRequired()
               .HasMaxLength(50);

    }
}
