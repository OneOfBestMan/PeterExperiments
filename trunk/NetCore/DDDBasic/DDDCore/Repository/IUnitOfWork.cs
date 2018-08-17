using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace LC.SDK.Core.Repository
{
    public interface IUnitOfWork
    {
        Task<int> Commit();
        //IRepository<T> Repository<T>() where T : class;
        void Rollback();
    }
}