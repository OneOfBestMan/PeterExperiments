using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DatabaseFirst.Models
{
    public partial class BlogifierContext : DbContext
    {
        public BlogifierContext()
        {
        }

        public BlogifierContext(DbContextOptions<BlogifierContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<BlogPosts> BlogPosts { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CustomFields> CustomFields { get; set; }
        public virtual DbSet<PostCategories> PostCategories { get; set; }
        public virtual DbSet<Profiles> Profiles { get; set; }
        public virtual DbSet<TongjiRoomArr> TongjiRoomArr { get; set; }

        // Unable to generate entity type for table 'dbo.TongjiSK07082'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.TongjiNanCourse'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.TongjiWeekdayRoom'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Blogifier;User Id=sa;Password=Pass@word;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Assets>(entity =>
            {
                entity.HasIndex(e => e.ProfileId);

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.ProfileId);
            });

            modelBuilder.Entity<BlogPosts>(entity =>
            {
                entity.HasIndex(e => e.ProfileId);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Image).HasMaxLength(160);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.HasOne(d => d.Profile)
                    .WithMany(p => p.BlogPosts)
                    .HasForeignKey(d => d.ProfileId);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(450);

                entity.Property(e => e.ImgSrc).HasMaxLength(160);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(160);
            });

            modelBuilder.Entity<CustomFields>(entity =>
            {
                entity.Property(e => e.CustomKey)
                    .IsRequired()
                    .HasMaxLength(140);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(160);
            });

            modelBuilder.Entity<PostCategories>(entity =>
            {
                entity.HasIndex(e => e.BlogPostId);

                entity.HasIndex(e => e.CategoryId);

                entity.HasOne(d => d.BlogPost)
                    .WithMany(p => p.PostCategories)
                    .HasForeignKey(d => d.BlogPostId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PostCategories)
                    .HasForeignKey(d => d.CategoryId);
            });

            modelBuilder.Entity<Profiles>(entity =>
            {
                entity.Property(e => e.AuthorEmail)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.AuthorName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Avatar).HasMaxLength(160);

                entity.Property(e => e.Bio).HasMaxLength(4000);

                entity.Property(e => e.BlogTheme)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.IdentityName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Image).HasMaxLength(160);

                entity.Property(e => e.Logo).HasMaxLength(160);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(160);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(160);
            });

            modelBuilder.Entity<TongjiRoomArr>(entity =>
            {
                entity.Property(e => e.Arrangement).HasMaxLength(500);

                entity.Property(e => e.RoomNum)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
