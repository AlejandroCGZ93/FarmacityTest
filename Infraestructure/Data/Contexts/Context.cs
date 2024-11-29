using Core.Entities;
using Infraestructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Infraestructure.Data.Contexts
{
    public class Context : DbContext
    {

        public Context(DbContextOptions<Context> options) : base(options) 
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        public override int SaveChanges(bool acceptAllChangesOnSucess)
        {
            return base.SaveChanges(acceptAllChangesOnSucess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureMapping(modelBuilder);

            Seeds.SeedData.Seed(modelBuilder);

        }

        private static void ConfigureMapping (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new BarcodeConfiguration());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Barcode> Barcodes { get; set; }
    }
}
