using Microsoft.EntityFrameworkCore;
using DB.Models;

namespace DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Album> Albums { get; set; }

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasIndex(p => p.ArtistName).IsUnique();

            modelBuilder.Entity<Artist>()
                .HasCheckConstraint("CK_Artists_ArtistName", "LEN([ArtistName]) > 0");


            modelBuilder.Entity<Genre>()
                .HasIndex(p => p.GenreName).IsUnique();

            modelBuilder.Entity<Genre>()
                .HasCheckConstraint("CK_Genres_GenreName", "LEN([GenreName]) > 0");


            modelBuilder.Entity<Album>()
                .HasIndex(p => new { p.AlbumName }).IsUnique();

            modelBuilder.Entity<Album>()
                .HasCheckConstraint("CK_Albums_AlbumName", "LEN([AlbumName]) > 0");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\University\\Year_4_(1)\\CSharp\\Lab2\\LabMainDB.mdf;Integrated Security=True;Connect Timeout=30");
        }
    }
}
