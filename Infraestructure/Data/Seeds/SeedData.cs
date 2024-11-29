using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Seeds
{
    public class SeedData
    {
        public static void Seed(ModelBuilder builder)
        {
            SeedApp(builder);  
        }

        private static void SeedApp(ModelBuilder builder)
        {
            builder.Entity<Barcode>().HasData(new List<Barcode>()
            {
                new() { Id = 1, ProductId = 1, Code = "AS132", IsActive = true, DateAdd = DateTime.Now, DateUpdate = DateTime.Now }
            });

            builder.Entity<Product>().HasData(new List<Product>()
            {
                new() { Id = 1, Name = "Product1", Price = 20.99M, QuantityStock = 10, IsActive = true, DateAdd = DateTime.Now, DateUpdate = DateTime.Now }
            });
        }

    }
}
