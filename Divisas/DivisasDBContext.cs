using Divisas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

        // Método para asegurarse de que la base de datos existe y se crea si no existe
        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }

        // Método para insertar datos de prueba
        public void SeedData()
        {
            if (!Monedas.Any())
            {
                Monedas.AddRange(
                    new Moneda { clave = "USD", valor_compra = 18.00, valor_venta = 19.00 },
                    new Moneda { clave = "EUR", valor_compra = 20.00, valor_venta = 21.00 },
                    new Moneda { clave = "JPY", valor_compra = 0.15, valor_venta = 0.16 }
                );

                SaveChanges();
            }

            if (!Sucursales.Any())
            {
                Sucursales.AddRange(
                    new Sucursal { nombre_empresa = "Sucursal Centro", direccion = "Calle 1, Centro", codigo_postal = 27000, ciudad = "Torreón", estado = "Coahuila" },
                    new Sucursal { nombre_empresa = "Sucursal Norte", direccion = "Calle 2, Norte", codigo_postal = 27000, ciudad = "Torreón", estado = "Coahuila" },
                    new Sucursal { nombre_empresa = "Sucursal Sur", direccion = "Calle 3, Sur" , codigo_postal = 27000 , ciudad = "Torreón" , estado = "Coahuila" }
                );

                SaveChanges();
            }
        }
    }
}
