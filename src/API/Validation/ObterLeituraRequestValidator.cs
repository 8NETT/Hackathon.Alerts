using API.Requests;

namespace API.Validation;

public class ObterLeituraRequestValidator : AbstractValidator<ObterLeituraRequest>
{
    public ObterLeituraRequestValidator()
    {
        RuleFor(r => r.TalhaoId)
            .NotEmpty()
            .WithMessage("TalhaoId é obrigatório.");
        RuleFor(r => r.Tipo)
            .NotEmpty()
            .WithMessage("Tipo é obrigatório.");
        RuleFor(r => r.Timestamp)
            .NotEmpty()
            .WithMessage("Timestamp é obrigatório.");
    }
}
