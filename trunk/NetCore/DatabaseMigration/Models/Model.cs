
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatabaseMigration.Models
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }

        public DbSet<Destination1> Destination1s { get; set; }
        public DbSet<Lodging1> Lodging1s { get; set; }

        public DbSet<Activity> Activitys { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<PersonPhoto> PersonPhotos { get; set; }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Trip>().HasMany(t => t.Activitys).WithOne(a => a.Trips).Map(m =>
            //{
            //    m.ToTable("TripActivities");
            //    m.MapLeftKey("TripIdentifier");//对应Trip的主键
            //    m.MapRightKey("ActivityId");
            //});

            //modelBuilder.Entity<Activity>().HasMany(a => a.Trips).WithMany(t => t.Activities).Map(m =>
            //{
            //    m.ToTable("TripActivities");
            //    m.MapLeftKey("ActivityId");//对应Activity的主键
            //    m.MapRightKey("TripIdentifier");
            //});

        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}