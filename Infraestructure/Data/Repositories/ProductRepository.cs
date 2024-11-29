using Core.Entities;
using Core.Repositories.Contracts;
using Infraestructure.Data.Contexts;
using Infraestructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Data.Repositories
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Product>> GetProductsActiveAsync()
        {
            var context = _context as Context;
            var productListActive = await context.Products
                .Where(p => p.IsActive)
                .Include(p => p.Barcode)  
                .ToListAsync();
            return productListActive;
        }

        public async Task<IEnumerable<Product>> GetAllWithBarcodeAsync()
        {
            var context = _context as Context;
            return await context.Products
                .Include(p => p.Barcode) 
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Product ID must be greater than zero");
            }

            try
            {
                var context = _context as Context;
                var product = await context.Products
                    .Include(p => p.Barcode)
                    .FirstOrDefaultAsync(p => p.Id == id);

                return product;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Database query failed", ex);
            }
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var context = _context as Context;
            // Agregar el producto a la base de datos
            await context.Products.AddAsync(product);

            // Guardar los cambios en la base de datos
            await context.SaveChangesAsync();

            // Devolver el producto con su barcode (si está relacionado)
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            var context = _context as Context;
         
            context.Products.Update(product);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var context = _context as Context;

            // Buscar el producto por ID
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                // Eliminar el producto junto con su código de barras (si existe)
                if (product.Barcode != null)
                {
                    context.Barcodes.Remove(product.Barcode);  // Eliminar código de barras
                }

                context.Products.Remove(product);  // Eliminar producto
                await context.SaveChangesAsync();  // Guardar los cambios en la base de datos
            }
        }



    }
}
