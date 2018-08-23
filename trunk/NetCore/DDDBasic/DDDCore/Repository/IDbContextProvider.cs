using Microsoft.EntityFrameworkCore;

namespace LC.SDK.Core.Repository
{
    public interface IDbContextProvider
    {
        DbContext Context { get; set; }

        void SetContext<T>(T context) where T : DbContext;
    }
}