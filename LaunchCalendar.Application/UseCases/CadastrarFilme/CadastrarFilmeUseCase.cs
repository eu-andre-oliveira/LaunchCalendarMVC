using LaunchCalendar.Domain.Abstractions.Repositories;
using LaunchCalendar.Domain.Entities;

namespace LaunchCalendar.Application.UseCases.CadastrarFilme
{
    public class CadastrarFilmeUseCase : ICadastrarFilmeUseCase
    {
        private readonly IFilmeRepository _filmeRepository; // Assumindo que você tenha um repositório para Filme

        public CadastrarFilmeUseCase(IFilmeRepository filmeRepository)
        {
            _filmeRepository = filmeRepository;
        }

        public void Execute(CadastrarFilmeInput input)
        {
            // Criar a entidade Filme
            var filme = new Filme
            {
                Titulo = input.Titulo,
                Genero = input.Genero,
                DataLancamento = input.DataLancamento,
                ImagemExibicao = input.ImagemExibicao
            };

            // Salvar no repositório
            _filmeRepository.Add(filme);
        }
    }
}
