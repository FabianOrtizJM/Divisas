using Divisas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divisas
{
    public class DivisasDbContext : DbContext
    {
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = ConexionDB.DevolverRuta("Divisas.db");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(col => col.id).IsRequired().ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(col => col.id).IsRequired().ValueGeneratedOnAdd();
            });
        }
    }

}
