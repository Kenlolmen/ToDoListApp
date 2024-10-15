using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Reflection.Emit;
using ToDoListApp.Models;
namespace ToDoListApp.Models
{
    public class ZadanieContext : DbContext
    {
        public ZadanieContext(DbContextOptions<ZadanieContext> options) : base(options) { }

        public DbSet<Status> Statusy { get; set; } = null;
        public DbSet<Kategoria> Kategorie { get; set; } = null;
        public DbSet<Zadanie> Zadania { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IDStatus); // Ustawienie klucza głównego
            });

            modelBuilder.Entity<Status>().HasData(
                  new Status { IDStatus = "zam", Nazwa = "Zrobione" },
                  new Status { IDStatus = "otw", Nazwa = "W trakcie" }

            );

            modelBuilder.Entity<Kategoria>(entity =>
            {
                entity.HasKey(e => e.IDKategoria); // Ustawienie klucza głównego
            });


            modelBuilder.Entity<Kategoria>().HasData(
                new Kategoria { IDKategoria = "pro", Nazwa = "Projekt" },
                new Kategoria { IDKategoria = "dom", Nazwa = "Dom" },
                new Kategoria { IDKategoria = "spo", Nazwa = "Sport" },
                new Kategoria { IDKategoria = "pra", Nazwa = "Praca" }
                );
        }
    }
}
