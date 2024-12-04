﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPIDemo.Data;

#nullable disable

namespace WebAPIDemo.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241125220740_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAPIDemo.Models.Shirt", b =>
                {
                    b.Property<int>("ShirtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShirtId"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("Size")
                        .HasColumnType("int");

                    b.HasKey("ShirtId");

                    b.ToTable("Shirts");

                    b.HasData(
                        new
                        {
                            ShirtId = 1,
                            Brand = "My Brand",
                            Color = "Blue",
                            Gender = "Men",
                            Price = 30.0,
                            Size = 10
                        },
                        new
                        {
                            ShirtId = 2,
                            Brand = "My Brand",
                            Color = "Black",
                            Gender = "Men",
                            Price = 35.0,
                            Size = 12
                        },
                        new
                        {
                            ShirtId = 3,
                            Brand = "Your Brand",
                            Color = "Red",
                            Gender = "Women",
                            Price = 28.0,
                            Size = 8
                        },
                        new
                        {
                            ShirtId = 4,
                            Brand = "Your Brand",
                            Color = "Yellow",
                            Gender = "Women",
                            Price = 30.0,
                            Size = 10
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
