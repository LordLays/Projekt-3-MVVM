using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KregulecApp.Model
{
    public class KregulecDatabaseContext : DbContext
    {
        public KregulecDatabaseContext() 
        {
        }

        public KregulecDatabaseContext(DbContextOptions<KregulecDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Convention> Conventions { get; set; }
        public DbSet<GameCatalog> GameCatalogs { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Tags> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(ConfigSetup.ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tags>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnName("Nazwa")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .HasColumnName("Opis")
                    .HasMaxLength(1000);
            });
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID");
                entity.Property(e => e.GameName)
                    .HasColumnName("Gra")
                    .IsRequired();
                entity.Property(e => e.Mark)
                    .HasColumnName("Oznaczenie")
                    .IsRequired();
                entity.Property(e => e.Rarity)
                    .HasColumnName("Rzadkosc")
                    .IsRequired();
                entity.Property(e => e.Completeness)
                    .HasColumnName("Kompletnosc")
                    .IsRequired();
                entity.Property(e => e.KnownMissingParts)
                    .HasColumnName("Brakujace_Czesci");
            });
            modelBuilder.Entity<Convention>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasColumnName("Nazwa");
                entity.Property(e => e.Location)
                    .HasColumnName("Lokalizacja")
                    .IsRequired();
                entity.Property(e => e.StartDate)
                    .HasColumnName("Data_Rozpoczecia")
                    .IsRequired();
                entity.Property(e => e.EndDate)
                    .HasColumnName("Data_Zakonczenia")
                    .IsRequired();
            });
            modelBuilder.Entity<GameCatalog>(entity =>
            {
                entity.Property(e => e.Title)
                    .HasColumnName("Tytul")
                    .IsRequired();
                entity.Property(e => e.Publisher)
                    .HasColumnName("Wydawca")
                    .IsRequired();
                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("Data_Wydania");
                entity.Property(e => e.Players)
                    .HasColumnName("Liczba_Graczy");
                entity.Property(e => e.PlayTime)
                    .HasColumnName("Czas_Gry");
                entity.Property(e => e.Age)
                    .HasColumnName("Wiek");
                entity.Property(e => e.Language)
                    .HasColumnName("Jezyk");
            });
        }
    }
}
