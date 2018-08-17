using Microsoft.EntityFrameworkCore;

namespace LC.SDK.Core.Repository
{

    public  class SDKDbContext : DbContext
    {
        public SDKDbContext(DbContextOptions<SDKDbContext> options) : base(options) { }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //ApplicationSettings.DatabaseOptions(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
