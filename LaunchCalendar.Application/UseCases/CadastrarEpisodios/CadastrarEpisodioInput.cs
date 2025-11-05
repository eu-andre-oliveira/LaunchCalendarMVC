using System;

namespace LaunchCalendar.Application.UseCases.CadastrarEpisodios
{
    public class CadastrarEpisodioInput
    {
        public string? Titulo { get; set; }
        public int Numero { get; set; }
        public int Temporada { get; set; }
        public int SerieId { get; set; }
        public DateTime DataLancamento { get; set; }
        public string? ImagemExibicao { get; set; }
    }
}