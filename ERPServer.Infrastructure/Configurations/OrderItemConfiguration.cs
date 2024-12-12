using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations
{
    internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // ShippingFee ve TaxAmount gibi decimal türündeki alanlar için precision ve scale belirtiyoruz
            builder.Property(o => o.Discount)
                .HasPrecision(18, 2);  // 18 toplam basamak, 2 ondalıklı basamak

            builder.Property(o => o.UnitPrice)
                .HasPrecision(18, 2);  // 18 toplam basamak, 2 ondalıklı basamak

        }
    }
}
