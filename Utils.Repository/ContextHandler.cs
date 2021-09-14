using Medallion.Threading.SqlServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Utils.Domain.Exceptions;
using Utils.Repository.Interfaces;

namespace Utils.Repository
{
    public abstract class BaseContextHandler<TContext> : IContextHandler where TContext : DbContext
    {
        private readonly TContext _context;
        private IDisposable _lockHandle;

        public BaseContextHandler(TContext context)
        {
            _context = context;
        }

        async Task<int> IContextHandler.SaveChangesAsync(CancellationToken cancellationToken)
        {
            int response = await _context.SaveChangesAsync(cancellationToken);

            if (_lockHandle != null)
            {
                _lockHandle.Dispose();
                _lockHandle = null;
            }

            _context.ChangeTracker.Clear();
            return response;
        }

        void IContextHandler.ClearTrackedChanges()
        {
            _context.ChangeTracker.Clear();
        }

        async Task IContextHandler.LockAsync(string lockKey)
        {
            var connection = _context.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open)
                connection.Open();

            var myLock = new SqlDistributedLock(lockKey, connection);

            var handle = await myLock.TryAcquireAsync();

            if (handle != null)
                _lockHandle = handle;
            else
                throw new LockTimeoutException("Lock waiting time expired");
        }

        void IContextHandler.DisposeLock(string lockKey)
        {
            if (_lockHandle != null)
            {
                _lockHandle.Dispose();
                _lockHandle = null;
            }
        }
    }
}