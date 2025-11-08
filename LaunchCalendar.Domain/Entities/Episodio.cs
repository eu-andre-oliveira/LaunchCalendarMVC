namespace LaunchCalendar.Domain.Entities
{
    public class Episodio
    {
        public int EpisodioId { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public string ImagemExibicao { get; set; }
        public int Numero { get; set; }
        public int Temporada { get; set; }
        public Serie? Serie { get; set; }
        public int SerieId { get; set; }
        public DateTime DataLancamento { get; set; }
    }
}