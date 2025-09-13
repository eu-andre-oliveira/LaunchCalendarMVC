namespace LaunchCalendar.Application.Queries.Filmes
{
    public class FilmeQueryOutput
    {
        public int FilmeId { get; set; }
        public string Titulo { get; set; }
        public string ImagemExibicao { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Genero { get; set; }
    }
}
