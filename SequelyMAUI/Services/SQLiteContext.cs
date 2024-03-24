using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SequelyMAUI.Entities;

namespace SequelyMAUI.Services
{
    internal class SQLiteContext : DbContext
    {
        public DbSet<ConnectionEntity> Connections { get; set; }
        public DbSet<TabEntity> Tabs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=sequely.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConnectionEntity>().HasMany(c => c.Databases).WithOne().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ConnectionEntity>().HasOne(c => c.ActualConnection).WithOne().OnDelete(DeleteBehavior.Cascade);
        }   
    }
}
