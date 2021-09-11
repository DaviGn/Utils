using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Utils.Services.Bases.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(object id);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<int> Count(bool incluirExcluidos = false);
        Task<int> Count(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<int> Count(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> orderPredicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IList<TEntity>> List(bool incluirExcluidos = false);
        Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IList<TEntity>> ListOrdered(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> orderPredicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task Add(TEntity model);
        Task Add(IList<TEntity> model);
        void Update(TEntity model);
        void Update(IList<TEntity> model);
        Task Remove(int id);
        void Remove(TEntity model);
        void Remove(IList<TEntity> models);
    }
}