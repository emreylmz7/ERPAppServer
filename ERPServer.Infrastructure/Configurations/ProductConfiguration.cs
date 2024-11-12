using ERPServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERPServer.Infrastructure.Configurations
{
    internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price)
                .HasPrecision(18, 2); // 18 toplam basamak, 2 ondalıklı basamak

            builder.Property(p => p.Cost)
                .HasPrecision(18, 2); 

            builder.Property(p => p.Name)
                .HasMaxLength(100) 
                .IsRequired();     

            builder.Property(p => p.Description)
                .HasMaxLength(500);
        }
    }
}
