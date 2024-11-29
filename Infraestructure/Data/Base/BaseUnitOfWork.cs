using Core.Repositories.Contracts.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure.Data.Base
{
    public abstract class BaseUnitOfWork : IBaseUnifOfWork, IDisposable
    {
        protected readonly DbContext _context;
        private IDbContextTransaction _currentTransaction;

        public BaseUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<T> TransactionallyDo<T>(Func<Task<T>> asyncAction, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            T result = default;
            var initializedTransaction = false;
            if (_currentTransaction == null)
            {
                _currentTransaction = await _context.Database.BeginTransactionAsync(isolationLevel);
                initializedTransaction = true;
            }
            try
            {
                result = await asyncAction();
                await CommitAsync();
            }
            catch (Exception ex)
            {
                if (initializedTransaction)
                {
                    await _currentTransaction.RollbackAsync();
                    _currentTransaction = null;
                }
                throw;
            }

            if (initializedTransaction)
            {
                await _currentTransaction.CommitAsync();
                _currentTransaction = null;
            }
            return result;
        }

        public async Task TransactionallyDo(Func<Task> asyncAction, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted)
        {
            var initializedTransaction = false;
            if (_currentTransaction == null)
            {
                _currentTransaction = await _context.Database.BeginTransactionAsync(isolationLevel);
                initializedTransaction = true;
            }
            try
            {
                await asyncAction();
                await CommitAsync();
            }
            catch(Exception)
            {
                if (initializedTransaction)
                {
                    await _currentTransaction.RollbackAsync();
                    _currentTransaction = null;
                }
                throw;
            }
            if (initializedTransaction)
            {
                await _currentTransaction.CommitAsync();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
    }
}
