using Model.Logs;
using Model.Organizations;
using Model.Payments;
using Model.Resouces;
using Model.System;
using Model.Users;
using Microsoft.EntityFrameworkCore;


namespace Data
{
    public class CCDbContext : DbContext
    {
        public CCDbContext(DbContextOptions<CCDbContext> options) : base(options) { }

        #region DB Set

        #region System
        public DbSet<Admin> Admins { get; set; }
        public DbSet<FriendLink> FriendLinks { get; set; }

        public DbSet<AdvCategory> AdvCategorys { get; set; }

        public DbSet<Model.System.Advertisement> Advertisements { get; set; }
        #endregion



        #region users
        public DbSet<BuyRecord> BuyRecords { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserExtend> UserExtends { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        #region Resouces

        public DbSet<ResouceCategory> ResouceCategorys { get; set; }
        ////public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ResouceExtend> ResouceExtends { get; set; }

        public DbSet<ResouceTag> ResouceTags { get; set; }
        public DbSet<Video> Videos { get; set; }

        public DbSet<AdminMenu> AdminMenus { get; set; }

        public DbSet<AttachConfig> AttachConfigs { get; set; }

        public DbSet<OtherConfig> OtherConfigs { get; set; }

        public DbSet<ResouceView> ResouceViews { get; set; }

        #endregion 

        #region Organization
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Domain> Domains { get; set; }

        #endregion

        #region Logs
        public DbSet<SystemLog> SystemLogs { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }

        #endregion

        #region Payment
        public DbSet<Payment> Payments { get; set; }
        #endregion

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
 
            //ApplicationSettings.DatabaseOptions(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>()
            .Property(b => b._Extend).HasColumnName("Extend");
            modelBuilder.Entity<Article>()
            .Property(b => b._Tags).HasColumnName("Tags");

            modelBuilder.Entity<Video>()
            .Property(b => b._Extend).HasColumnName("Extend");
            modelBuilder.Entity<Video>()
            .Property(b => b._Tags).HasColumnName("Tags");

            modelBuilder.Entity<Course>()
          .Property(b => b._Extend).HasColumnName("Extend");
            modelBuilder.Entity<Course>()
            .Property(b => b._Tags).HasColumnName("Tags");
            modelBuilder.Entity<Course>()
         .Property(b => b._CourseItems).HasColumnName("CourseItems");

            modelBuilder.Entity<Course>().HasOne(p => p.Category).WithMany();

            modelBuilder.Entity<Article>().HasOne(p => p.Category).WithMany();

            modelBuilder.Entity<Advertisement>().HasOne(p => p.Category).WithMany();

        }
    }
}
