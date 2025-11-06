using System;

namespace LaunchCalendar.Application.Queries.Episodios
{
    public class EpisodiosQueryOutput
    {
        public int EpisodioId { get; set; }
        public string Titulo { get; set; }
        public int Numero { get; set; }
        public int Temporada { get; set; }
        public int SerieId { get; set; }
        public string? SerieTitulo { get; set; } // nova propriedade
        public string? ImagemExibicao { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}