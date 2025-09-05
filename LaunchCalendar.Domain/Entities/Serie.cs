namespace LaunchCalendar.Domain.Entities
{
    public class Serie
    {
        public int SerieId { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int QtdeTemporadas { get; set; }
        public ICollection<Episodio> Episodios { get; set; }
    }
}