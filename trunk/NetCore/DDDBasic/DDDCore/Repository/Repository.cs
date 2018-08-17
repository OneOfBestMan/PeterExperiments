using LC.SDK.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LC.SDK.Core.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> _entities;
        protected readonly IDbContext _context;
        //protected readonly UnitOfWork _unitOfWork;

        public Repository(IDbContext dbContext)
        {
            _context = dbContext;
            _entities = _context.Set<TEntity>();
            //_unitOfWork = new UnitOfWork(_context);
        }
        #region 查(返回最终结果)
        public TEntity GetById(string id, bool isNotTrack = true)
        {
            if (isNotTrack)
            {
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            return _entities.Find(id);
        }
        public TEntity GetInclude<T>(Expression<Func<TEntity, bool>> predicate, string propertyName, bool isNotTrack = true)
        {
            if (isNotTrack)
            {
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            return _entities.Include(propertyName).Single(predicate);
        }

        public async Task<TEntity> GetByIdAsync(string id, bool isNotTrack = true)
        {
            if (isNotTrack)
            {
                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
            return await _entities.FindAsync(id);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true)
        {
            if (isNotTrack)
            {
                return _entities.AsNoTracking().SingleOrDefault(predicate);
            }
            return _entities.SingleOrDefault(predicate);
        }
        public TEntity SingleInclude(Expression<Func<TEntity, bool>> predicate, string propertyName, bool isNotTrack = true)
        {
            if (isNotTrack)
            {
                return _entities.AsNoTracking().SingleOrDefault(predicate);
            }
            if (!string.IsNullOrEmpty(propertyName))
            {
                return _entities.Include(propertyName).SingleOrDefault(predicate);
            }
            return _entities.SingleOrDefault(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true)
        {
            if (isNotTrack)
            {
                return await _entities.AsNoTracking().SingleOrDefaultAsync(predicate);
            }
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> Query(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true)
        {
            return _entities.AsNoTracking().Where(predicate);
        }

        public IEnumerable<TEntity> QueryInclude(Expression<Func<TEntity, bool>> predicate, string propertyName, bool isNotTrack = true)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _entities.AsNoTracking().Where(predicate);
            }
            return _entities.Include(propertyName).AsNoTracking().Where(predicate);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> predicate, bool isNotTrack = true)
        {
            return await _entities.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IEnumerable<TEntity> Filter(out int count, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null,
  int? pageSize = null)
        {
            IQueryable<TEntity> query = _entities.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query.ToList();
        }

        public IEnumerable<TEntity> FilterInclude(out int count, string propertyName, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null,
         int? pageSize = null)
        {
            IQueryable<TEntity> query = null;
            if (string.IsNullOrEmpty(propertyName))
            {
                query = _entities.AsNoTracking();
            }
            else
            {
                query = _entities.Include(propertyName).AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            count = query.Count();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query.ToList();
        }

        public IEnumerable<TEntity> FilterByQuery(out int count, IQueryable<TEntity> query = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? page = null,
         int? pageSize = null)
        {
            count = query.Count();
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query.ToList();
        }

        public IEnumerable<TEntity> All(bool isNotTrack = true)
        {
            return _entities.AsNoTracking().ToList();
        }
        public IEnumerable<TEntity> AllInclude(string propertyName, bool isNotTrack = true)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _entities.AsNoTracking().ToList();
            }
            return _entities.Include(propertyName).AsNoTracking().ToList();
        }

        public async Task<ICollection<TEntity>> AllAsync(bool isNotTrack = true)
        {
            return await _entities.AsNoTracking().ToListAsync();
        }
        #endregion

        #region 统计
        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AsNoTracking().Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.AsNoTracking().CountAsync(predicate);
        }
        #endregion

        #region 增删改
        public void AddVoid(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public TEntity Add(TEntity entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
            return await _context.SaveChangesAsync();
            //  _context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            _entities.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
            _context.SaveChanges();
        }

        public async Task<int> UpdateRangeAysnc(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
            return await _context.SaveChangesAsync();
            //_context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(TEntity t)
        {
            _entities.Remove(t);
            return await _context.SaveChangesAsync();
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }
        public async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region 返回IQueryable

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
            {
                return _entities.AsNoTracking().Where(predicate);
            }
            else
            {
                return _entities.AsNoTracking();
            }
        }
        public IQueryable<TEntity> FindByInclude(Expression<Func<TEntity, bool>> predicate, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _entities.AsNoTracking().Where(predicate);
            }
            return _entities.Include(propertyName).AsNoTracking().Where(predicate);
        }

        #endregion

        #region 执行sql操作命令
        public void Execute(string sql)
        {
            _context.Database.ExecuteSqlCommand(sql);
        }

        public async Task<int> ExecuteAsync(string sql)
        {
            return await _context.Database.ExecuteSqlCommandAsync(sql);
        }
        #endregion

        public bool Exist(Expression<Func<TEntity, bool>> predicate)
        {
            var exist = _entities.AsNoTracking().Where(predicate);
            return exist.Any() ? true : false;
        }

        protected void AddBaseDefault<T>(T entity) where T : ModelBase<string>
        {
            entity.CreationTime = DateTime.Now;
        }
        protected void AddFullAuditDefault<T>(T entity) where T : ModelFullAudit<string>
        {
            entity.CreationTime = DateTime.Now;
            entity.IsDeleted = false;
        }


    }
}
