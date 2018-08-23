using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Data
{
    public interface IUnitOfWork
    {

        Task<int> Commit();
        IRepository<T> Repository<T>() where T : class;
        void Rollback();
    }
}