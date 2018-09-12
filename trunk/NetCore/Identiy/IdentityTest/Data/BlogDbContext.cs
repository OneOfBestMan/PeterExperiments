using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityDemo.Models;

namespace IdentityDemo.Data
{
 
    public class BlogDbContext : IdentityDbContext<User, Role, string>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        #region DB Sets
        //public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<UserClaim> UserClaims { get; set; }
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
