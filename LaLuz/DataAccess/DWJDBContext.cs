using System;
using LaLuz.Models;
using LaLuz.Utils;
using Microsoft.EntityFrameworkCore;

namespace LaLuz.DataAccess
{
    public class DWJDBContext : DbContext
    {
        public DbSet<DataWhitJson> DataWhitJsons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dBConection = $"Filename={DBConection.GetDatabaseRoute("LaLuz.db")}";
            optionsBuilder.UseSqlite(dBConection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataWhitJson>(entity =>
            {
                entity.HasKey(col => col.Id);
                entity.Property(col => col.Id).IsRequired().ValueGeneratedOnAdd();
            });
        }
        // Método para limpiar la tabla
        public async Task<bool> ClearDataWhitJsonsTableAsync()
        {
            try
            {
                await Database.ExecuteSqlRawAsync("DELETE * FROM DataWhitJsons");
                SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

    }
}

