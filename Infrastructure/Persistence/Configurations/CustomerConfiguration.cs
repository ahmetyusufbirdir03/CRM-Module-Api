using Bogus;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(11)
            .IsFixedLength();

        builder.HasIndex(c => c.PhoneNumber)
            .IsUnique();

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(c => c.City)
            .HasMaxLength(50);

        builder.Property(c => c.Country)
            .HasMaxLength(50);

        builder.Property(c => c.Address)
            .HasMaxLength(200);

        builder.HasOne(c => c.CustomerType)
            .WithMany()
            .HasForeignKey(c => c.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.CustomerState)
            .WithMany()
            .HasForeignKey(c => c.StateId)
            .OnDelete(DeleteBehavior.Restrict);


        Faker fakr = new();

        Customer customer1 = new()
        {
            Id = 1,
            FirstName = "Ahmet Yusuf",
            LastName = "Birdir",
            PhoneNumber = "05558978804",
            Email = "ahmet@mail.com",
            City = "Bursa",
            Country = "Türkiye",
            Address = "Hamitler mah.",
            TypeId = 4,
            StateId = 3,
        };

        Customer customer2 = new()
        {
            Id = 2,
            FirstName = "Zeynep Kübra",
            LastName = "Birdir",
            PhoneNumber = "05558232304",
            Email = "zeynep@mail.com",
            City = "Bursa",
            Country = "Türkiye",
            Address = "Hamitler mah.",
            TypeId = 1,
            StateId = 1,
        };

        Customer customer3 = new()
        {
            Id = 3,
            FirstName = "Fatma Zehra",
            LastName = "Birdir",
            PhoneNumber = "05308195351",
            Email = "fatma@mail.com",
            City = "Bursa",
            Country = "Türkiye",
            Address = "Hamitler mah.",
            TypeId = 2,
            StateId = 2,
        };

        builder.HasData(customer1,customer2,customer3);

    }
}
