using System.Linq.Expressions;

namespace Core.Repositories.Contracts.Base
{
    public interface IRepository<TEntity, TIdentifier>
    {

        ValueTask<TEntity> Get(TIdentifier id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsNoTrack();
        Task<IEnumerable<TEntity>>GetAll(params Expression<Func<TEntity, bool>>[] predicates);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(TIdentifier id);
        Task<int>Count();
        Task<int>Count(Expression<Func<TEntity, bool>> predicate);
        Task<bool>Any(Expression<Func<TEntity, bool>> predicate);
    }
}
