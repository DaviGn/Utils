using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Utils.Services.Bases.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(object id, CancellationToken cancellationToken = default);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<int> Count(bool incluirExcluidos = false, CancellationToken cancellationToken = default);
        Task<int> Count(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<int> Count(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> orderPredicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IList<TEntity>> List(bool incluirExcluidos = false, CancellationToken cancellationToken = default);
        Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> wherePredicate, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> ListOrdered(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> orderPredicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task Add(TEntity model, CancellationToken cancellationToken = default);
        Task Add(IList<TEntity> model, CancellationToken cancellationToken = default);
        void Update(TEntity model);
        void Update(IList<TEntity> model);
        Task Remove(int id);
        void Remove(TEntity model);
        void Remove(IList<TEntity> models);
    }
}