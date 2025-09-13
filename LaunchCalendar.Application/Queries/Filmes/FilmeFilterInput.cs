namespace LaunchCalendar.Application.Queries.Filmes
{
    public class FilmeFilterInput
    {
        public string? Titulo { get; set; }
        public string? Genero { get; set; }
        public DateTime? DataLancamentoInicio { get; set; }
        public DateTime? DataLancamentoFim { get; set; }
    }
}