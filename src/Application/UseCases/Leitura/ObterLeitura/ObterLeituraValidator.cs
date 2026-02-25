using Domain.ValueObjects;

namespace Application.UseCases.Leitura.ObterLeitura;

internal sealed class ObterLeituraValidator : AbstractValidator<ObterLeituraDTO>
{
    public ObterLeituraValidator()
    {
        RuleFor(d => d.TalhaoId)
            .NotEmpty()
            .WithMessage("O talhão é obrigatório.");

        RuleFor(d => d.Tipo)
            .NotEmpty()
            .WithMessage("O tipo é obrigatório.")
            .Must(tipo => TipoSensor.TryParse(tipo, out _))
            .WithMessage("Tipo inválido.");

        RuleFor(d => d.Timestamp)
            .Must(ts => ts != default)
            .WithMessage("O Timestamp é obrigatório.")
            .LessThanOrEqualTo(DateTimeOffset.Now.AddMinutes(5))
            .WithMessage("O Timestamp não pode estar no futuro.");
    }
}
