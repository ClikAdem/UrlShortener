using Microsoft.EntityFrameworkCore;
using UrlShortener.Model;

namespace UrlShortener.Data
{
    public partial class UrlDbContext : DbContext
    {
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>().ToTable("Url");
        }
    }
}
