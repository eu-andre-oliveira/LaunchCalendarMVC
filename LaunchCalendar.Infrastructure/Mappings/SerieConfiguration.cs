using LaunchCalendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaunchCalendar.Infrastructure.Mappings
{
    public class SerieConfiguration : IEntityTypeConfiguration<Serie>
    {
        public void Configure(EntityTypeBuilder<Serie> builder)
        {
            builder.ToTable("Series");

            builder.HasKey(s => s.SerieId);

            builder.Property(s => s.Titulo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Genero)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.QtdeTemporadas)
                .IsRequired();

            builder.HasMany(s => s.Episodios)
                .WithOne(e => e.Serie)
                .HasForeignKey(e => e.SerieId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}