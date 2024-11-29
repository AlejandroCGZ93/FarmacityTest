using Core.Entities;
using Infraestructure.Data.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class BarcodeConfiguration : BaseEntityConfiguration<Barcode>
    {
        public override void Configure(EntityTypeBuilder<Barcode> builder)
        {
            builder.ToTable("Barcodes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(255);

            builder.Property(x => x.IsActive)
               .IsRequired();

            builder.Property(x => x.DateAdd)
                .IsRequired();

            builder.Property(x => x.DateUpdate)
                .IsRequired();

            builder.HasOne(x => x.Product)
             .WithOne(p => p.Barcode) 
             .HasForeignKey<Barcode>(x => x.ProductId)  
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
