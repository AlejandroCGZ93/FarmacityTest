using Core.Entities;
using Infraestructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)"); 

            builder.Property(x => x.QuantityStock)
                .IsRequired();

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.Property(x => x.DateAdd)
                .IsRequired();

            builder.Property(x => x.DateUpdate)
                .IsRequired();

            builder.HasOne(p => p.Barcode)
           .WithOne(b => b.Product)
           .HasForeignKey<Barcode>(b => b.ProductId);

        }
    }
}
