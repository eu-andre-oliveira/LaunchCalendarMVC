using LaunchCalendar.Application.UseCases.CadastrarFilme;

namespace LaunchCalendar.Application.UseCases.CadastrarSerie
{
    public interface ICadastrarSerieUseCase
    {
        void Execute(CadastrarSerieInput input);
    }
}
