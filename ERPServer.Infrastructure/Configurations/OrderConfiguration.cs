using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // ShippingFee ve TaxAmount gibi decimal türündeki alanlar için precision ve scale belirtiyoruz
            builder.Property(o => o.ShippingFee)
                .HasPrecision(18, 2);  // 18 toplam basamak, 2 ondalıklı basamak
            
            builder.Property(o => o.TaxRate)
                .HasPrecision(18, 2);  // 18 toplam basamak, 2 ondalıklı basamak

            builder.Property(o => o.TotalAmount)
                .HasPrecision(18, 2);  // 18 toplam basamak, 2 ondalıklı basamak

            builder.Property(o => o.SubTotal)
                .HasPrecision(18, 2);  // 18 toplam basamak, 2 ondalıklı basamak

            builder.Property(o => o.TaxAmount)
                .HasPrecision(18, 2);  // 18 toplam basamak, 2 ondalıklı basamak


            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Restrict); // veya DeleteBehavior.NoAction

            builder.HasOne(o => o.ShippingAddress)
                   .WithMany()
                   .HasForeignKey(o => o.ShippingAddressId)
                   .OnDelete(DeleteBehavior.Restrict); // veya DeleteBehavior.NoAction

        }
    }
}
