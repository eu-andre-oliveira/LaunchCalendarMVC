using LaunchCalendar.Application.Queries.Filmes;

namespace LaunchCalendar.Application.Queries.Filmes
{
    public interface IFilmeQuery
    {
        IEnumerable<FilmeQueryOutput> ListarTodos();
        IEnumerable<FilmeQueryOutput> ListarComFiltro(FilmeFilterInput filter);
    }
}