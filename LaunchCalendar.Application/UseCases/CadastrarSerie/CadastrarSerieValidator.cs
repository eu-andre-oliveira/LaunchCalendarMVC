using FluentValidation;

namespace LaunchCalendar.Application.UseCases.CadastrarSerie
{
    public class CadastrarSerieValidator : AbstractValidator<CadastrarSerieInput>
    {
        public CadastrarSerieValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .Length(1, 100).WithMessage("O título deve ter entre 1 e 100 caracteres.");

            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("O gênero é obrigatório.")
                .Length(1, 50).WithMessage("O gênero deve ter entre 1 e 50 caracteres.");

            RuleFor(x => x.QtdeTemporadas)
                .GreaterThan(0).WithMessage("A quantidade de temporadas deve ser maior que 0.");
        }
    }
}
