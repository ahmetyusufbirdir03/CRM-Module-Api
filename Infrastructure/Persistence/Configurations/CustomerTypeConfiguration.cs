using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerType>
{
    public void Configure(EntityTypeBuilder<CustomerType> builder)
    {

        builder.HasKey(ct => ct.Id);

        builder.Property(c => c.Type)
            .HasMaxLength(20)
            .IsRequired();

       
    }
}
