using System.Threading;
using System.Threading.Tasks;

namespace Utils.Repository.Interfaces
{
    public interface IContextHandler
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task LockAsync(string lockKey);
        void ClearTrackedChanges();
        void DisposeLock(string lockKey);
    }
}