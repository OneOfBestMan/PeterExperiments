using Microsoft.EntityFrameworkCore;

namespace LC.SDK.Core.Repository
{
    public class DbContextProvider : IDbContextProvider
    {
        public DbContext Context { get; set; }

        
        public void SetContext<T>(T context) where T : DbContext
        {
            Context = context;
        }
    }
}
