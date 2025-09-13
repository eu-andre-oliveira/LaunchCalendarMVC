using LaunchCalendar.Domain.Abstractions.Repositories;

namespace LaunchCalendar.Application.Queries.Filmes
{
    public class FilmeQuery : IFilmeQuery
    {
        private readonly IFilmeRepository _filmeRepository;

        public FilmeQuery(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public IEnumerable<FilmeQueryOutput> ListarTodos()
        {
            return _filmeRepository.GetAll()
                .Select(f => new FilmeQueryOutput
                {
                    FilmeId = f.FilmeId,
                    Titulo = f.Titulo,
                    ImagemExibicao = f.ImagemExibicao,
                    DataLancamento = f.DataLancamento,
                    Genero = f.Genero
                });
        }

        public IEnumerable<FilmeQueryOutput> ListarComFiltro(FilmeFilterInput filter)
        {
            var query = _filmeRepository.GetAll().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Titulo))
                query = query.Where(f => f.Titulo.Contains(filter.Titulo));

            if (!string.IsNullOrWhiteSpace(filter.Genero))
                query = query.Where(f => f.Genero.Contains(filter.Genero));

            if (filter.DataLancamentoInicio.HasValue)
                query = query.Where(f => f.DataLancamento >= filter.DataLancamentoInicio.Value);

            if (filter.DataLancamentoFim.HasValue)
                query = query.Where(f => f.DataLancamento <= filter.DataLancamentoFim.Value);

            return [.. query.Select(f => new FilmeQueryOutput
            {
                FilmeId = f.FilmeId,
                Titulo = f.Titulo,
                ImagemExibicao = f.ImagemExibicao,
                DataLancamento = f.DataLancamento,
                Genero = f.Genero
            })];
        }
    }
}