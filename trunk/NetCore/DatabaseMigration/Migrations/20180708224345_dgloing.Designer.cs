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
    [Migration("20180708224345_dgloing")]
    partial class dgloing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("LodgingId");

                    b.HasIndex("DestinationId");

                    b.ToTable("Lodgings");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Lodging1", b =>
                {
                    b.Property<int>("Lodging1Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Destination1Id");

                    b.Property<int?>("DestinationId");

                    b.Property<bool>("IsResort");

                    b.Property<decimal>("MilesFromNearestAirport");

                    b.Property<string>("Name");

                    b.Property<string>("Owner");

                    b.HasKey("Lodging1Id");

                    b.HasIndex("DestinationId");

                    b.ToTable("Lodging1s");
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

            modelBuilder.Entity("DatabaseMigration.Models.Lodging", b =>
                {
                    b.HasOne("DatabaseMigration.Models.Destination", "Destination")
                        .WithMany("Lodgings")
                        .HasForeignKey("DestinationId");
                });

            modelBuilder.Entity("DatabaseMigration.Models.Lodging1", b =>
                {
                    b.HasOne("DatabaseMigration.Models.Destination", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationId");
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
