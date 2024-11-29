using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories.Contracts;
using Core.Repositories.Contracts.UnitOfWork;
using Core.Services.Contracts;
using Core.Services.Implementations.Base;


namespace Core.Services.Implementations
{
    public class ProductService : BaseService<ProductDto, Product, int, IProductRepository>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork productUnitOfWork)
             : base(productUnitOfWork, productUnitOfWork.Product, mapper)
        {
            _mapper = mapper;
            _unitOfWork = productUnitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetActiveProducts()
        {
            var activeProducts = await _repository.GetProductsActiveAsync();
            return activeProducts.Select(MapProductToDto);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _repository.GetAllWithBarcodeAsync();
            return products.Select(MapProductToDto);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            // Verificación de ID válido
            if (id <= 0)
            {
                throw new ArgumentException("El ID del producto debe ser mayor que cero");
            }

            // Obtén el producto
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
            {
                throw new BusinessNotFoundException($"Producto con ID {id} no encontrado");
            }

            // Retorna el DTO si el producto existe
            return MapProductToDto(product);
        }


        public async Task<ProductDto> CreateOrUpdateProductAsync(ProductDto productDto)
        {
            Product product;
        
            if (productDto.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }

            if (productDto.Id == 0 && productDto.Barcode?.Id == 0)
            {
                product = CreateProductFromDto(productDto);
                var createdProduct = await _unitOfWork.Product.CreateAsync(product);
                return MapProductToDto(createdProduct);
            }
            else
            {
                var existingProduct = await _unitOfWork.Product.GetByIdAsync(productDto.Id);
                if (existingProduct == null) return null;

                UpdateProductFromDto(existingProduct, productDto);
                await _unitOfWork.Product.UpdateAsync(existingProduct);
                return MapProductToDto(existingProduct);
            }
        }

        public async Task<ProductDto> DeleteAsync(int id)
        {
            var product = await _unitOfWork.Product.GetByIdAsync(id);
            if (product == null) return null;

            await _unitOfWork.Product.DeleteAsync(id);
            return MapProductToDto(product);
        }

        private ProductDto MapProductToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                QuantityStock = product.QuantityStock,
                IsActive = product.IsActive,
                DateAdd = product.DateAdd,
                DateUpdate = product.DateUpdate,
                Barcode = product.Barcode != null ? new BarcodeDto
                {
                    Id = product.Barcode.Id,
                    Code = product.Barcode.Code,
                    IsActive = product.Barcode.IsActive,
                    DateAdd = product.Barcode.DateAdd,
                    DateUpdate = product.Barcode.DateUpdate
                } : null
            };
        }

        private Product CreateProductFromDto(ProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                QuantityStock = productDto.QuantityStock,
                IsActive = productDto.IsActive,
                DateAdd = DateTime.Now,
                DateUpdate = DateTime.Now,
                Barcode = new Barcode
                {
                    Code = productDto.Barcode.Code,
                    IsActive = productDto.Barcode.IsActive,
                    DateAdd = DateTime.Now,
                    DateUpdate = DateTime.Now
                }
            };
        }

        private void UpdateProductFromDto(Product product, ProductDto productDto)
        {
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.QuantityStock = productDto.QuantityStock;
            product.IsActive = productDto.IsActive;
            product.DateUpdate = DateTime.Now;

            if (product.Barcode != null)
            {
                product.Barcode.Code = productDto.Barcode.Code;
                product.Barcode.IsActive = productDto.Barcode.IsActive;
                product.Barcode.DateUpdate = DateTime.Now;
            }
            else
            {
                product.Barcode = new Barcode
                {
                    Code = productDto.Barcode.Code,
                    IsActive = productDto.Barcode.IsActive,
                    DateUpdate = DateTime.Now
                };
            }
        }
    }
}
