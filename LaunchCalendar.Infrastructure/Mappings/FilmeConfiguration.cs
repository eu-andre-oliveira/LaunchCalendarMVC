using LaunchCalendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaunchCalendar.Infrastructure.Mappings
{
    public class FilmeConfiguration : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.ToTable("Filmes");

            builder.HasKey(f => f.FilmeId);

            builder.Property(f => f.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Genero)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(f => f.DataLancamento)
                .IsRequired();

            builder.Property(f => f.ImagemExibicao)
                .HasMaxLength(2000);
        }
    }
}