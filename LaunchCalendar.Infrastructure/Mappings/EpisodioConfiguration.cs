using LaunchCalendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaunchCalendar.Infrastructure.Mappings
{
    public class EpisodioConfiguration : IEntityTypeConfiguration<Episodio>
    {
        public void Configure(EntityTypeBuilder<Episodio> builder)
        {
            builder.ToTable("Episodios");

            builder.HasKey(e => e.EpisodioId);

            builder.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Descricao)
                .HasMaxLength(500);

            builder.Property(e => e.Numero)
                .IsRequired();

            builder.Property(e => e.Temporada)
                .IsRequired();

            builder.Property(e => e.DataLancamento)
                .IsRequired();

            builder.Property(e => e.ImagemExibicao)
                .HasMaxLength(2000);

            builder.HasOne(e => e.Serie)
                .WithMany(s => s.Episodios)
                .HasForeignKey(e => e.SerieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
