using Domain.ValueObjects;

namespace Application.UseCases.Regra.CadastrarRegra;

internal sealed class CadastrarRegraValidator : AbstractValidator<CadastrarRegraDTO>
{
    public CadastrarRegraValidator()
    {
        RuleFor(c => c.Tipo)
            .NotEmpty()
            .WithMessage("Tipo é obrigatório.")
            .Must(tipo => TipoSensor.TryParse(tipo, out var _))
            .WithMessage("Tipo inválido.");

        RuleFor(c => c.Alvo)
            .NotEmpty()
            .WithMessage("Alvo é obrigatório.")
            .Must(alvo => EstatisticaAlvo.TryParse(alvo, out var _))
            .WithMessage("Alvo inválido.");

        RuleFor(c => c.Operador)
            .NotEmpty()
            .WithMessage("Operador é obrigatório.")
            .Must(operador => OperadorComparacao.TryParse(operador, out var _))
            .WithMessage("Operador inválido.");

        RuleFor(c => c.Limite)
            .NotEmpty()
            .WithMessage("Limite é obrigatório.")
            .Must(v => !double.IsNaN(v) && !double.IsInfinity(v))
            .WithMessage("Valor inválido.");

        RuleFor(c => c.JanelasConsecutivas)
            .NotEmpty()
            .WithMessage("JanelasConsecutivas é obrigatório.")
            .GreaterThanOrEqualTo(1)
            .WithMessage("JanelasConsecutivas deve ser maior ou igual a 1.");

        RuleFor(c => c.ExigirJanelaCompleta)
            .NotEmpty()
            .WithMessage("ExigirJanelaCompleta é obrigatório.");

        RuleFor(c => c.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.")
            .Length(1, 50)
            .WithMessage("Nome deve ter entre 1 a 50 caracteres.");

        RuleFor(c => c.Severidade)
            .NotEmpty()
            .WithMessage("Severidade é obrigatório.")
            .Must(severidade => Severidade.TryParse(severidade, out var _))
            .WithMessage("Severidade inválida.");
    }
}
