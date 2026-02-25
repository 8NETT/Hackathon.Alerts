using Domain.Entities;

namespace Domain.Validation;

internal sealed class RegraDeAlertaValidator : AbstractValidator<RegraDeAlerta>
{
    public RegraDeAlertaValidator()
    {
        RuleFor(r => r.Tipo)
            .NotEmpty()
            .WithMessage("Tipo é obrigatório.");
        RuleFor(r => r.Alvo)
            .NotEmpty()
            .WithMessage("Alvo é obrigatório.");
        RuleFor(r => r.Operador)
            .NotEmpty()
            .WithMessage("Operador é obrigatório.");
        RuleFor(r => r.Limite)
            .Must(l => !double.IsNaN(l) && !double.IsInfinity(l))
            .WithMessage("Limite inválido.");
        RuleFor(r => r.JanelasConsecutivas)
            .GreaterThanOrEqualTo(1)
            .WithMessage("JanelasConsecutivas deve ser maior ou igual a 1.");
        RuleFor(r => r.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.");
        RuleFor(r => r.Severidade)
            .NotEmpty()
            .WithMessage("Severidade é obrigatório.");
    }
}
