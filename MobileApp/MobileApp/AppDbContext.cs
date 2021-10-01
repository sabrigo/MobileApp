using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using SQLite;
using Xamarin.Forms;

namespace MobileApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<LocationData> LocationData { get; set; }
        public DbSet<Setting> Setting { get; set; }

        public AppDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(GetDBPath());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationData>()
                .Property(e => e.Latitude)
                .HasConversion<double>();
            modelBuilder.Entity<LocationData>()
                .Property(e => e.Longitude)
                .HasConversion<double>();
        }

        private string GetDBPath()
        {
            var dbFileName = "rscamalertsample.db";
            var databasePath = Xamarin.Essentials.FileSystem.AppDataDirectory;
            var fullPath = Path.Combine(databasePath, dbFileName);
            return $"Data Source={fullPath}";
        }
    }

    public class Setting
    {
        [PrimaryKey] public int Id { get; set; }
        public string DistanceInMeters { get; set; }
        public string ContinuousAlertDistance { get; set; }
        public bool IsContinuousAlert { get; set; }
    }

    public class LocationData
    {
        public LocationData(Guid id, double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Id = id;
        }

        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}