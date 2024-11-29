using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Contracts.Base
{
    public interface IService<TDto, TEntity, TIdentifier>
           where TDto : class
           where TEntity : class
    {
        Task<TDto>GetById(TIdentifier id);
        Task<TDto> Get(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TDto>> GetAll();
        Task<IEnumerable<TDto>>GetAllNoTrack();
        Task<IEnumerable<TDto>> GetAll(params Expression<Func<TEntity, bool>>[] filters);
        Task<TDto> Add(TDto model);
        Task Update(TDto model);
        Task Delete(TIdentifier id);
        Task<bool>Exists(Expression<Func<TEntity, bool>> predicate);
    }
}
