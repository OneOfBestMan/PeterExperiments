using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LC.SDK.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void AddVoid(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> All(bool isNotTrack = true);
        IEnumerable<TEntity> AllInclude(string propertyName, bool isNotTrack = true);
        Task<ICollection<TEntity>> AllAsync(bool isNotTrack = true);
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        void Delete(TEntity entity);
        Task<int> DeleteAsync(TEntity t);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);
        void Execute(string sql);
        Task<int> ExecuteAsync(string sql);
        bool Exist(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Filter(out int count, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null,
  int? pageSize = null);

        IEnumerable<TEntity> FilterInclude(out int count, string propertyName, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null,
         int? pageSize = null);

        IEnumerable<TEntity> FilterByQuery(out int count, IQueryable<TEntity> query = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null,
        int? pageSize = null);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, string propertyName);
        TEntity GetById(string id, bool isNotTrack = true);
        TEntity GetInclude<T>(Expression<Func<TEntity, bool>> predicate, string propertyName, bool isNotTrack = true);
        Task<TEntity> GetByIdAsync(string id, bool isNotTrack = true);
        IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true);
        IEnumerable<TEntity> QueryInclude(Expression<Func<TEntity, bool>> predicate, string propertyName, bool isNotTrack = true);
        Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true);
        TEntity Single(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true);
        TEntity SingleInclude(Expression<Func<TEntity, bool>> predicate, string propertyName, bool isNotTrack = true);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true);
        TEntity Update(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        Task<int> UpdateRangeAysnc(IEnumerable<TEntity> entities);
    }
}
