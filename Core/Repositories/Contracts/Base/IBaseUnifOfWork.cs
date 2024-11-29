
namespace Core.Repositories.Contracts.Base
{
    public interface IBaseUnifOfWork
    {
        Task<T> TransactionallyDo<T>(Func<Task<T>> asyncAction, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted);
        Task TransactionallyDo(Func<Task> asyncAction, System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted);
        Task<int> CommitAsync();
    }
}
