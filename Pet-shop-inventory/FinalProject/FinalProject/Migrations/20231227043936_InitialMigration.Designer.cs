﻿// <auto-generated />
using System;
using FinalProject.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinalProject.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231227043936_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FinalProject.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin",
                            Password = "123456"
                        });
                });

            modelBuilder.Entity("FinalProject.Entities.CageOrAquarium", b =>
                {
                    b.Property<int>("CageOrAquariumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CageOrAquariumId"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CageOrAquariumId");

                    b.ToTable("CagesOrAquariums");
                });

            modelBuilder.Entity("FinalProject.Entities.FeedingSchedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<int>("CageOrAquariumId")
                        .HasColumnType("int");

                    b.Property<string>("FeedTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScheduleId");

                    b.HasIndex("CageOrAquariumId");

                    b.ToTable("FeedingSchedules");
                });

            modelBuilder.Entity("FinalProject.Entities.Pet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CageOrAquariumId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CageOrAquariumId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("FinalProject.Entities.PurchaseInformation", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseId"));

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("PurchasePrice")
                        .HasColumnType("float");

                    b.Property<string>("SellerContactInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SellerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfPet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PurchaseId");

                    b.ToTable("PurchaseInformations");
                });

            modelBuilder.Entity("FinalProject.Entities.SalesRecord", b =>
                {
                    b.Property<int>("SalesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SalesId"));

                    b.Property<string>("CustomerContactInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("SalePrice")
                        .HasColumnType("float");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SalesId");

                    b.ToTable("SalesRecords");
                });

            modelBuilder.Entity("FinalProject.Entities.FeedingSchedule", b =>
                {
                    b.HasOne("FinalProject.Entities.CageOrAquarium", "CageOrAquarium")
                        .WithMany("FeedingSchedules")
                        .HasForeignKey("CageOrAquariumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CageOrAquarium");
                });

            modelBuilder.Entity("FinalProject.Entities.Pet", b =>
                {
                    b.HasOne("FinalProject.Entities.CageOrAquarium", "CageOrAquarium")
                        .WithMany("Pets")
                        .HasForeignKey("CageOrAquariumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CageOrAquarium");
                });

            modelBuilder.Entity("FinalProject.Entities.CageOrAquarium", b =>
                {
                    b.Navigation("FeedingSchedules");

                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
