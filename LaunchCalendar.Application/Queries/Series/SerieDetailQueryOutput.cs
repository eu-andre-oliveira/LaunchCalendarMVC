using System.Collections.Generic;
using LaunchCalendar.Application.Queries.Episodios;

namespace LaunchCalendar.Application.Queries.Series
{
    public class SerieDetailQueryOutput
    {
        public int SerieId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public int QtdeTemporadas { get; set; }

        // Lista de episódios relacionados (usa o DTO já criado)
        public IEnumerable<EpisodiosQueryOutput> Episodios { get; set; } = new List<EpisodiosQueryOutput>();
    }
}