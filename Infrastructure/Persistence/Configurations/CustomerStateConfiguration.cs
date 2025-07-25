using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CustomerStateConfiguration : IEntityTypeConfiguration<CustomerState>
{
    public void Configure(EntityTypeBuilder<CustomerState> builder)
    {
        builder.ToTable("CustomerStates");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.State)
            .IsRequired(true)
            .HasMaxLength(20);

        
    }
}
