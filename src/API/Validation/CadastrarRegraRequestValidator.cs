using API.Requests;

namespace API.Validation;

internal sealed class CadastrarRegraRequestValidator : AbstractValidator<CadastrarRegraRequest>
{
    public CadastrarRegraRequestValidator()
    {
        RuleFor(c => c.Tipo)
            .NotNull()
            .WithMessage("Tipo é obrigatório.");

        RuleFor(c => c.Alvo)
            .NotNull()
            .WithMessage("Alvo é obrigatório.");

        RuleFor(c => c.Operador)
            .NotNull()
            .WithMessage("Operador é obrigatório.");

        RuleFor(c => c.Limite)
            .NotNull()
            .WithMessage("Limite é obrigatório.");

        RuleFor(c => c.JanelasConsecutivas)
            .NotNull()
            .WithMessage("JanelasConsecutivas é obrigatório.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("JanelasConsecutivas deve ser maior ou igual a 0.");

        RuleFor(c => c.ExigirJanelaCompleta)
            .NotNull()
            .WithMessage("ExigirJanelaCompleta é obrigatório.");

        RuleFor(c => c.Nome)
            .NotNull()
            .WithMessage("Nome é obrigatório.")
            .Length(1, 50)
            .WithMessage("Nome deve ter entre 1 a 50 caracteres.");

        RuleFor(c => c.Severidade)
            .NotNull()
            .WithMessage("Severidade é obrigatório.");
    }
}
