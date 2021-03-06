﻿// <auto-generated />
using System;
using DatabaseMigration.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseMigration.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20180708231930_one_to_one")]
    partial class one_to_one
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DatabaseMigration.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("TripId");

                    b.HasKey("ActivityId");

                    b.HasIndex("TripId");

                    b.ToTable("Activitys");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Destination", b =>
                {
                    b.Property<int>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Photo");

                    b.HasKey("DestinationId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Destination1", b =>
                {
                    b.Property<int>("Destination1Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Photo");

                    b.HasKey("Destination1Id");

                    b.ToTable("Destination1s");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Lodging", b =>
                {
                    b.Property<int>("LodgingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DestinationId");

                    b.Property<bool>("IsResort");

                    b.Property<decimal>("MilesFromNearestAirport");

                    b.Property<string>("Name");

                    b.Property<string>("Owner");

                    b.Property<int?>("PrimaryContactPersonId");

                    b.Property<int?>("SecondaryContactPersonId");

                    b.HasKey("LodgingId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("PrimaryContactPersonId");

                    b.HasIndex("SecondaryContactPersonId");

                    b.ToTable("Lodgings");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Lodging1", b =>
                {
                    b.Property<int>("Lodging1Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Destination1122Id");

                    b.Property<bool>("IsResort");

                    b.Property<decimal>("MilesFromNearestAirport");

                    b.Property<string>("Name");

                    b.Property<string>("Owner");

                    b.HasKey("Lodging1Id");

                    b.HasIndex("Destination1122Id");

                    b.ToTable("Lodging1s");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastName");

                    b.Property<string>("Name");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<int>("SocialSecurityNumber");

                    b.HasKey("PersonId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("DatabaseMigration.Models.PersonPhoto", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<string>("Caption");

                    b.Property<byte[]>("Photo");

                    b.HasKey("PersonId");

                    b.ToTable("PersonPhotos");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId");

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("CostUSD");

                    b.Property<DateTime>("EndDate");

                    b.Property<byte[]>("RowVersion");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("TripId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Activity", b =>
                {
                    b.HasOne("DatabaseMigration.Models.Trip")
                        .WithMany("Activitys")
                        .HasForeignKey("TripId");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Lodging", b =>
                {
                    b.HasOne("DatabaseMigration.Models.Destination", "Destination")
                        .WithMany("Lodgings")
                        .HasForeignKey("DestinationId");

                    b.HasOne("DatabaseMigration.Models.Person", "PrimaryContact")
                        .WithMany()
                        .HasForeignKey("PrimaryContactPersonId");

                    b.HasOne("DatabaseMigration.Models.Person", "SecondaryContact")
                        .WithMany()
                        .HasForeignKey("SecondaryContactPersonId");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Lodging1", b =>
                {
                    b.HasOne("DatabaseMigration.Models.Destination1", "Destination")
                        .WithMany()
                        .HasForeignKey("Destination1122Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseMigration.Models.PersonPhoto", b =>
                {
                    b.HasOne("DatabaseMigration.Models.Person", "PhotoOf")
                        .WithOne("Photo")
                        .HasForeignKey("DatabaseMigration.Models.PersonPhoto", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DatabaseMigration.Models.Post", b =>
                {
                    b.HasOne("DatabaseMigration.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
