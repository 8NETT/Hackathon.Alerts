using Domain.ValueObjects;

namespace Domain.Parsing;

internal static class SeveridadeParser
{
    public static Severidade Parse(string? value)
    {
        if (TryParse(value, out var severidade))
            return severidade!;

        throw new ArgumentException("Severidade inválida.");
    }

    public static bool TryParse(string? value, out Severidade? severidade)
    {
        switch (value?.Trim().ToUpperInvariant())
        {
            case "A":
            case "ALTA":
                severidade = Severidade.Alta; return true;
            case "M":
            case "MEDIA":
            case "MÉDIA":
                severidade = Severidade.Media; return true;
            case "B":
            case "BAIXA":
                severidade = Severidade.Baixa; return true;
            default:
                severidade = null;
                return false;
        }
    }
}
