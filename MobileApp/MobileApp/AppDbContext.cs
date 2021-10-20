using Microsoft.EntityFrameworkCore;
using SQLite;
using System.IO;

namespace MobileApp
{
    public class AppDbContext : DbContext
    {
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

}