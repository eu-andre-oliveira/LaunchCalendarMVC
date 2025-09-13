namespace LaunchCalendar.Application.UseCases.CadastrarFilme
{
    public class CadastrarFilmeInput 
    {
        public string Titulo { get; set; }
        public string ImagemExibicao { get; set; }
        public DateTime DataLancamento { get; set; }
        public string Genero { get; set; }
    }
}
