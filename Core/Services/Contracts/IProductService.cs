using Core.Dtos;
using Core.Entities;
using Core.Services.Contracts.Base;


namespace Core.Services.Contracts
{
    public interface IProductService : IService<ProductDto, Product, int>
    {
        public  Task<IEnumerable<ProductDto>> GetActiveProducts();
        public  Task<IEnumerable<ProductDto>> GetAllProducts();
        public Task<ProductDto> GetByIdAsync(int id);
        public Task<ProductDto> CreateOrUpdateProductAsync(ProductDto productDto);
        public  Task<ProductDto> DeleteAsync(int id);
    }
}
