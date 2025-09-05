using LaunchCalendar.Domain.Entities;
using LaunchCalendar.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace LaunchCalendar.Infrastructure.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Episodio> Episodios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FilmeConfiguration());
            modelBuilder.ApplyConfiguration(new SerieConfiguration());
            modelBuilder.ApplyConfiguration(new EpisodioConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
