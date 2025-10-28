using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Application.Queries.Episodios
{
    public class EpisodiosFilterInput
    {
        public string? Titulo { get; set; }
        public int? Temporada { get; set; }
        public int SerieId { get; set; }
        public DateTime? DataLancamento { get; set; }
    }
}
