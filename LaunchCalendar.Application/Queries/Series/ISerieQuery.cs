using System.Collections.Generic;

namespace LaunchCalendar.Application.Queries.Series
{
    public interface ISerieQuery
    {
        IEnumerable<SerieQueryOutput> ListarTodos();
        IEnumerable<SerieQueryOutput> ListarComFiltro(SerieFilterInput filter);

        // Retorna detalhe de uma série (inclui episódios)
        SerieDetailQueryOutput? ObterPorId(int id);
    }
}