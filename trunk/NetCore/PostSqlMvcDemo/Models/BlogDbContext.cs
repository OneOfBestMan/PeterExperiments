using Microsoft.EntityFrameworkCore;
using NetCoreTest.Models.DataModel;

namespace NetCoreTest.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        #region DB Sets
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        #endregion

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
