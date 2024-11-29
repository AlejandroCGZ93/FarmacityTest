using Core.Repositories.Contracts;
using Core.Repositories.Contracts.UnitOfWork;
using Infraestructure.Data.Base;
using Infraestructure.Data.Contexts;
using Infraestructure.Data.Repositories;


namespace Infraestructure.Data.UnitOfWorks
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        private IProductRepository _productRepository;

        public UnitOfWork(Context context) : base(context) 
        { 
        }

        public IProductRepository Product => _productRepository ??= new ProductRepository(_context);
    }
}
