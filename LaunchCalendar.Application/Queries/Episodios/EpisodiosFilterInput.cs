using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Application.Queries.Episodios
{
    public class EpisodiosFilterInput
    {
        public string? Titulo { get; set; }
        public int? Temporada { get; set; }
        public int SerieId { get; set; }

        // existing single date (kept for compatibility)
        public DateTime? DataLancamento { get; set; }

        // Add range properties used by the controller
        public DateTime? DataLancamentoInicio { get; set; }
        public DateTime? DataLancamentoFim { get; set; }
    }
}
