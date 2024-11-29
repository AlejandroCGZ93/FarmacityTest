using Core.Entities;
using Core.Repositories.Contracts.Base;


namespace Core.Repositories.Contracts
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<IEnumerable<Product>> GetProductsActiveAsync();
        public Task<IEnumerable<Product>> GetAllWithBarcodeAsync();
        public  Task<Product> GetByIdAsync(int id);
        public Task<Product> CreateAsync(Product product);
        public  Task UpdateAsync(Product product);
        public  Task DeleteAsync(int id);
    }
}
