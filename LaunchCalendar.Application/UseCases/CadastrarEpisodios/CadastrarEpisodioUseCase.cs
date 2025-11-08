using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Application.UseCases.CadastrarEpisodios
{
    public class CadastrarEpisodioUseCase : ICadastrarEpisodioUseCase
    {
        private readonly IEpisodioRepository _episodioRepository;

        public CadastrarEpisodioUseCase(IEpisodioRepository episodioRepository)
        {
            _episodioRepository = episodioRepository;
        }

        public void Execute(CadastrarEpisodioInput input)
        {
            var episodio = new Episodio
            {
                Titulo = input.Titulo ?? string.Empty,
                Descricao = input.Descricao,
                Numero = input.Numero,
                Temporada = input.Temporada,
                SerieId = input.SerieId,
                DataLancamento = input.DataLancamento,
                ImagemExibicao = input.ImagemExibicao ?? string.Empty
            };

            _episodioRepository.Add(episodio);
        }
    }
}