using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Application.UseCases.CadastrarSerie
{
    public class CadastrarSerieUseCase : ICadastrarSerieUseCase
    {
        private readonly ISerieRepository _serieRepository; // Assumindo que você tenha um repositório para Serie

        public CadastrarSerieUseCase(ISerieRepository serieRepository)
        {
            _serieRepository = serieRepository;
        }

        public void Execute(CadastrarSerieInput input)
        {
            // Criar a entidade Serie
            var serie = new Serie
            {
                Titulo = input.Titulo,
                Genero = input.Genero,
                QtdeTemporadas = input.QtdeTemporadas
            };

            // Salvar no repositório
            _serieRepository.Add(serie);
        }
    }
}
