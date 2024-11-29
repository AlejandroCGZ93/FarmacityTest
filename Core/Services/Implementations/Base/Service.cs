using AutoMapper;
using Core.Entities.Base;
using Core.Exceptions;
using Core.Repositories.Contracts.Base;
using Core.Services.Contracts.Base;
using System.Linq.Expressions;


namespace Core.Services.Implementations.Base
{
    public abstract class BaseService<TDto, TEntity, TIdentifier, TRepository> : IService<TDto, TEntity, TIdentifier>
        where TDto : class
        where TEntity : Entity<TIdentifier>
        where TRepository : IRepository<TEntity, TIdentifier>
    {
        private readonly IBaseUnifOfWork unifOfWork;
        protected readonly TRepository _repository;
        protected readonly IMapper ServiceMapper;

        protected BaseService(IBaseUnifOfWork unifOfWork, TRepository repository, IMapper serviceMapper)
        {
            this.unifOfWork = unifOfWork;
            _repository = repository;
            ServiceMapper = serviceMapper;
        }

        public virtual async Task<TDto> Add(TDto dto)
        {
            var entity = await _repository.Add(ServiceMapper.Map<TEntity>(dto));
            await unifOfWork.CommitAsync();
            return ServiceMapper.Map<TDto>(entity);
        }

        public virtual Task Delete(TIdentifier id)
        {
            _repository.Remove(id);
            return Task.CompletedTask;
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Any(predicate);
        }

        public virtual async Task<TDto> GetById(TIdentifier id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                throw new BusinessNotFoundException();
            
            return ServiceMapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await _repository.FirstOrDefault(predicate);
            if (result == null)
                throw new BusinessNotFoundException();
            return ServiceMapper.Map<TDto>(result);
        }

        public virtual async Task<IEnumerable<TDto>> GetAll()
        {
            return ServiceMapper.Map<TDto[]>(await _repository.GetAll());
        }

        public virtual async Task<IEnumerable<TDto>> GetAllNoTrack()
        {
            return ServiceMapper.Map<TDto[]>(await _repository.GetAllAsNoTrack());
        }

        public async Task<IEnumerable<TDto>> GetAll(params Expression<Func<TEntity, bool>>[] predicate)
        {
            IEnumerable<TEntity> entities;

            if (predicate != null)
                entities = await _repository.GetAll(predicate);
            else
                entities = await _repository.GetAll();
            
            return ServiceMapper.Map<TDto[]>(entities);
        }

        public virtual Task Update(TDto dto)
        {
            _repository.Update(ServiceMapper.Map<TEntity>(dto));
            return Task.CompletedTask;
        }
    }
}
