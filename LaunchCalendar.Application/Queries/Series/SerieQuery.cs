using LaunchCalendar.Domain.Abstractions.Repositories;

namespace LaunchCalendar.Application.Queries.Series
{
    public class SerieQuery : ISerieQuery
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
    }
}
