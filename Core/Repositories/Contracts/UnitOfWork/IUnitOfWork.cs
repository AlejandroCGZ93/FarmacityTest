using Core.Repositories.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IBaseUnifOfWork
    {
        IProductRepository Product { get; }
    }
}
