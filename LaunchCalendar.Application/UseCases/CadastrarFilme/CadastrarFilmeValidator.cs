using FluentValidation;

namespace LaunchCalendar.Application.UseCases.CadastrarFilme
{
    public class CadastrarFilmeValidator : AbstractValidator<CadastrarFilmeInput>
    {
        public CadastrarFilmeValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .Length(1, 100).WithMessage("O título deve ter entre 1 e 100 caracteres.");

            RuleFor(x => x.Genero)
                .NotEmpty().WithMessage("O Genero é obrigatória.")
                .Length(1, 50).WithMessage("O Genero deve ter entre 1 e 50 caracteres.");

            RuleFor(x => x.DataLancamento)
                .NotEmpty().WithMessage("A data de lançamento é obrigatória.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de lançamento não pode ser no futuro.");

            RuleFor(x => x.ImagemExibicao)
                .NotEmpty().WithMessage("A URL da imagem é obrigatória.")
                .Length(1, 200).WithMessage("A URL da imagem deve ter entre 1 e 200 caracteres.")
                .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute)).WithMessage("A URL da imagem deve ser válida.");
        }
    }
}
