using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utils.Domain.Interfaces;
using Utils.Domain.Models.Bases;
using Utils.Repository;

namespace Utils.Services.Bases.Interfaces
{
    public class BaseService<TContext, TEntity> : IBaseService<TEntity> where TContext : DbContext
                                                                        where TEntity : class, IBaseModel
    {
        protected readonly TContext _context;

        protected bool isDeleteable => typeof(IDeleteable).IsAssignableFrom(typeof(TEntity));
        protected bool isBaseModel = typeof(BaseModel).IsAssignableFrom(typeof(TEntity));

        public BaseService(TContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> GetById(object id)
        {
            if (!isBaseModel || !isDeleteable)
                return await _context.Set<TEntity>().FindAsync(id);

            return await _context.Set<TEntity>().FindAsync(x => (x as BaseModel).Id == (int)id && (isDeleteable ? !((IDeleteable)x).Excluido : true));
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await CreateQuery(wherePredicate, includeProperties).FirstOrDefaultAsync();

        public virtual async Task<TEntity> GetAny(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await CreateQueryAny(wherePredicate, includeProperties).FirstOrDefaultAsync();

        public virtual async Task<int> Count(bool incluirExcluidos = false)
        => await CreateListQuery(incluirExcluidos).AsNoTracking().CountAsync();

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await CreateQuery(wherePredicate, includeProperties).CountAsync();

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> orderPredicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await CreateQuery(wherePredicate, orderPredicate, includeProperties).CountAsync();

        public virtual async Task<IList<TEntity>> List(bool incluirExcluidos = false)
        => await CreateListQuery(incluirExcluidos).AsNoTracking().ToListAsync();

        public virtual async Task<IList<TEntity>> List(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await CreateQuery(wherePredicate, includeProperties).ToListAsync();

        public virtual async Task<IList<TEntity>> ListOrdered(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> orderPredicate, params Expression<Func<TEntity, object>>[] includeProperties)
            => await CreateQuery(wherePredicate, orderPredicate, includeProperties).ToListAsync();

        private IQueryable<TEntity> CreateListQuery(bool incluirExcluidos = false)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            if (!incluirExcluidos && isDeleteable)
                query = query.Where(x => !((IDeleteable)x).Excluido);

            return query;
        }

        private IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>> wherePredicate, Expression<Func<TEntity, object>> orderPredicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = CreateQuery(wherePredicate, includeProperties);

            if (orderPredicate != null)
                query = query.OrderBy(orderPredicate);

            return query.AsNoTracking();
        }

        private IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            query = query.Where(wherePredicate);

            if (isDeleteable)
                query = query.Where(x => !((IDeleteable)x).Excluido);

            foreach (var property in includeProperties)
                query = query.Include(property);

            return query.AsNoTracking();
        }

        private IQueryable<TEntity> CreateQueryAny(Expression<Func<TEntity, bool>> wherePredicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _context.Set<TEntity>().AsQueryable();

            query = query.Where(wherePredicate);

            foreach (var property in includeProperties)
                query = query.Include(property);

            return query.AsNoTracking();
        }

        public virtual async Task Add(TEntity model)
        {
            await _context.Set<TEntity>().AddAsync(model);
        }

        public virtual async Task Add(IList<TEntity> models)
        {
            await _context.Set<TEntity>().AddRangeAsync(models);
        }

        public virtual void Update(TEntity model)
        {
            _context.Set<TEntity>().Update(model);
        }

        public virtual void Update(IList<TEntity> models)
        {
            _context.Set<TEntity>().UpdateRange(models);
        }

        public virtual async Task Remove(int id)
        {
            var model = await GetById(id);

            if (model != null)
                Remove(model);
        }

        public virtual void Remove(TEntity model)
        {
            if (model is IDeleteable)
            {
                (model as IDeleteable).Excluido = true;
                Update(model);
            }
            else
                _context.Set<TEntity>().Remove(model);
        }

        public virtual void Remove(IList<TEntity> models)
        {
            foreach (var item in models)
                Remove(item);
        }
    }
}
