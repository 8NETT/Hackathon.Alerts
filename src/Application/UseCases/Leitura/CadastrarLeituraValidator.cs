using Domain.Parsing;

namespace Application.UseCases.Leitura;

internal sealed class CadastrarLeituraValidator : AbstractValidator<CadastrarLeituraDTO>
{
    public CadastrarLeituraValidator()
    {
        RuleFor(d => d.TalhaoId)
            .NotEmpty()
            .WithMessage("O SensorId é obrigatório.");

        RuleFor(d => d.Tipo)
            .NotEmpty()
            .WithMessage("O Tipo é obrigatório.")
            .Must(tipo => TipoSensorParser.TryParse(tipo, out _))
            .WithMessage("Tipo inválido.");

        RuleFor(d => d.Valor)
            .Must(v => !double.IsNaN(v) && !double.IsInfinity(v))
            .WithMessage("Valor inválido.");

        RuleFor(d => d.Timestamp)
            .Must(ts => ts != default)
            .WithMessage("O Timestamp é obrigatório.")
            .LessThanOrEqualTo(DateTimeOffset.Now.AddMinutes(5))
            .WithMessage("O Timestamp não pode estar no futuro.");
    }
}
