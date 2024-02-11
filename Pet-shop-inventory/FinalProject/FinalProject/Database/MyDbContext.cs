using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Database
{
    public class MyDbContext : DbContext
    {
        private readonly string _connectionString;
        public MyDbContext()
        {
            _connectionString = "Server =.\\SQLEXPRESS; Database = CSharpB15; User Id = csharpb15; Password = 123456; Trust Server Certificate = True";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
                optionsBuilder.UseSqlServer(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasData(GetAdmin());

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.CageOrAquarium)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.CageOrAquariumId);

            modelBuilder.Entity<FeedingSchedule>()
                .HasOne(f => f.CageOrAquarium)
                .WithMany(c => c.FeedingSchedules)
                .HasForeignKey(f => f.CageOrAquariumId);
        }
        private Admin GetAdmin()
        {
            return new Admin
            {
                Id = 1,
                Name = "admin",
                Password = "123456"
            };
        }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<CageOrAquarium> CagesOrAquariums { get; set; }
        public DbSet<FeedingSchedule> FeedingSchedules { get; set; }
        public DbSet<PurchaseInformation> PurchaseInformations { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
    }
}
