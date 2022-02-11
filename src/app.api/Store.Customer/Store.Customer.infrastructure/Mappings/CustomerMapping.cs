using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Customers.Domain.Entities;
using Store.Shared.Core.ValueObjects;

namespace Store.Customers.Infrastructure.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.CPF, tf =>
            {
                tf.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(CPF.MaxLength)
                    .HasColumnName("CPF")
                    .HasColumnType($"varchar({CPF.MaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.EmailAddress)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.MaxLength})");
            });

            // 1 : 1 => Customer : Address
            builder.HasOne(c => c.Address)
                .WithOne(c => c.Customer);

            builder.ToTable("Customers");
        }
    }
}
