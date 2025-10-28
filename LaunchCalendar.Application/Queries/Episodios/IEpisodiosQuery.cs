namespace LaunchCalendar.Application.Queries.Episodios
{
    public interface IEpisodiosQuery
    {
        IEnumerable<EpisodiosQueryOutput> ListarComFiltro(EpisodiosFilterInput filter);
    }
}
