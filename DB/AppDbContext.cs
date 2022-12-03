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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\University\\Year_4_(1)\\CSharp\\Lab2\\LabDb.mdf;Integrated Security=True;Connect Timeout=30");
        }
    }
}
