using System.Linq;
using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Application.Queries.Episodios;

namespace LaunchCalendar.Application.Queries.Series
{
    public partial class SerieQuery : ISerieQuery
    {
        private readonly ISerieRepository _serieRepository;

        public SerieQuery(ISerieRepository serieRepository)
        {
            _serieRepository = serieRepository;
        }

        public IEnumerable<SerieQueryOutput> ListarTodos()
        {
            return _serieRepository.GetAll()
                .Select(s => new SerieQueryOutput
                {
                    SerieId = s.SerieId,
                    Titulo = s.Titulo,
                    Genero = s.Genero,
                    QtdeTemporadas = s.QtdeTemporadas
                });
        }

        public IEnumerable<SerieQueryOutput> ListarComFiltro(SerieFilterInput filter)
        {
            var query = _serieRepository.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Titulo))
                query = query.Where(s => s.Titulo.Contains(filter.Titulo));

            if (!string.IsNullOrWhiteSpace(filter.Genero))
                query = query.Where(s => s.Genero.Contains(filter.Genero));

            if (filter.QtdeTemporadas.HasValue)
                query = query.Where(s => s.QtdeTemporadas == filter.QtdeTemporadas.Value);

            return query.Select(s => new SerieQueryOutput
            {
                SerieId = s.SerieId,
                Titulo = s.Titulo,
                Genero = s.Genero,
                QtdeTemporadas = s.QtdeTemporadas
            }).ToList();
        }

        public SerieDetailQueryOutput? ObterPorId(int id)
        {
            // Project from repository queryable to avoid relying on navigation property Includes.
            var query = _serieRepository.GetAll().AsQueryable();

            var detalhe = query
                .Where(s => s.SerieId == id)
                .Select(s => new SerieDetailQueryOutput
                {
                    SerieId = s.SerieId,
                    Titulo = s.Titulo,
                    Genero = s.Genero,
                    QtdeTemporadas = s.QtdeTemporadas,
                    Episodios = s.Episodios
                        .OrderBy(e => e.DataLancamento)
                        .Select(e => new EpisodiosQueryOutput
                        {
                            EpisodioId = e.EpisodioId,
                            Titulo = e.Titulo,
                            Numero = e.Numero,
                            Temporada = e.Temporada,
                            SerieId = e.SerieId,
                            SerieTitulo = e.Serie != null ? e.Serie.Titulo : null,
                            ImagemExibicao = e.ImagemExibicao,
                            DataLancamento = e.DataLancamento
                        })
                        .ToList()
                })
                .FirstOrDefault();

            return detalhe;
        }
    }
}
