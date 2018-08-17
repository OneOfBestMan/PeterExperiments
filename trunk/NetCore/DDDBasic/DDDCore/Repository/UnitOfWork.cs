using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 

namespace LC.SDK.Core.Repository
{
    public class UnitOfWork: IUnitOfWork  //: IUnitOfWork where 
    {
        private readonly DbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public IRepository<T> Repository<T>() where T : class
        //{
        //    if (Repositories.Keys.Contains(typeof(T)))
        //    {
        //        return Repositories[typeof(T)] as IRepository<T>;
        //    }

        //    IRepository<T> repo = new Repository<T>(new  DbContextProvider<DbContext>());
        //    Repositories.Add(typeof(T), repo);
        //    return repo;
        //}

        public async Task<int> Commit()
        {
           return await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
    }
}
