using Core.Repositories.Contracts.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infraestructure.Data.Repositories.Base
{
    public abstract class BaseRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
        where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected virtual IQueryable<TEntity> LoadRelations(IQueryable<TEntity> query)
        {
            return query;
        }

        protected BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        protected IQueryable<T> ApplyFilters<T>(IQueryable<T> source, Expression<Func<T, bool>>[] filters)
        {
            foreach (var filter in filters)
            {
                source = source.Where(filter);
            }
            return source;
        }
        public async Task<IEnumerable<TEntity>> GetAll(params Expression<Func<TEntity, bool>>[] predicates)
        {
            return await ApplyFilters(LoadRelations(_dbSet), predicates).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await LoadRelations(_dbSet).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsNoTrack()
        {
            return await LoadRelations(_dbSet).AsNoTracking().ToListAsync();
        }

        protected virtual Task<TEntity> LoadReferences(TEntity entity)
        {
            return Task.FromResult(entity);
        }
        public virtual async ValueTask<TEntity> Get(TIdentifier id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
                await LoadReferences(entity);
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
             _dbSet.Remove(entity);
        }

        public virtual void Remove(TIdentifier id)
        {
            var existing = _context.Find<TEntity>(id);
            if(existing != null) _dbSet.Remove(existing);
        }

        public virtual async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await LoadRelations(_dbSet).FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<int> Count()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
