using LaunchCalendar.Domain.Abstractions.Repositories;

namespace LaunchCalendar.Application.Queries.Episodios
{
    public class EpisodiosQuery : IEpisodiosQuery
    {
        private readonly IEpisodioRepository _episodioRepository;

        public EpisodiosQuery(IEpisodioRepository episodioRepository)
        {
            _episodioRepository = episodioRepository;
        }

        public IEnumerable<EpisodiosQueryOutput> ListarComFiltro(EpisodiosFilterInput filter)
        {
            var query = _episodioRepository.GetAll().AsQueryable();

            if (filter == null)
                filter = new EpisodiosFilterInput();

            if (!string.IsNullOrWhiteSpace(filter.Titulo))
                query = query.Where(e => e.Titulo != null && e.Titulo.Contains(filter.Titulo, StringComparison.OrdinalIgnoreCase));

            if (filter.Temporada.HasValue)
                query = query.Where(e => e.Temporada == filter.Temporada.Value);

            if (filter.SerieId > 0)
                query = query.Where(e => e.SerieId == filter.SerieId);

            if (filter.DataLancamentoInicio.HasValue)
                query = query.Where(e => e.DataLancamento >= filter.DataLancamentoInicio.Value);

            if (filter.DataLancamentoFim.HasValue)
                query = query.Where(e => e.DataLancamento <= filter.DataLancamentoFim.Value);

            if (filter.DataLancamento.HasValue)
            {
                var date = filter.DataLancamento.Value.Date;
                query = query.Where(e => e.DataLancamento.Date == date);
            }

            return query
                .Select(e => new EpisodiosQueryOutput
                {
                    EpisodioId = e.EpisodioId,
                    Titulo = e.Titulo,
                    Numero = e.Numero,
                    Temporada = e.Temporada,
                    SerieId = e.SerieId,
                    ImagemExibicao = e.ImagemExibicao,
                    DataLancamento = e.DataLancamento
                })
                .ToList();
        }
    }
}
