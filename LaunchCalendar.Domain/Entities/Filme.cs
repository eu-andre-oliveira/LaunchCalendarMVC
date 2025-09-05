namespace LaunchCalendar.Domain.Entities
{
    public class Filme
    {
        public int FilmeId { get; set; }
        public string Titulo { get; set; }
        public string ImagemExibicao { get; set; }        
        public DateTime DataLancamento { get; set; }
        public string Genero { get; set; }
    }
}