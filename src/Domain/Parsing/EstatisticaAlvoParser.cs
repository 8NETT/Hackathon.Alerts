using Domain.ValueObjects;

namespace Domain.Parsing;

internal static class EstatisticaAlvoParser
{
    public static EstatisticaAlvo Parse(string? value)
    {
        if (TryParse(value, out var estatistica))
            return estatistica!;

        throw new ArgumentException("EstatisticaAlvo inválida.");
    }

    public static bool TryParse(string? value, out EstatisticaAlvo? estatistica)
    {
        switch (value?.Trim().ToUpperInvariant())
        {
            case "MIN":
            case "MINIMA":
            case "MÍNIMA":
                estatistica = EstatisticaAlvo.Minima; return true;
            case "MAX":
            case "MAXIMA":
            case "MÁXIMA":
                estatistica = EstatisticaAlvo.Maxima; return true;
            case "MED":
            case "MEDIA":
            case "MÉDIA":
                estatistica = EstatisticaAlvo.Media; return true;
            case "SUM":
            case "SOMA":
                estatistica = EstatisticaAlvo.Soma; return true;
            case "LAST":
            case "ULTIMO":
            case "ÚLTIMO":
            case "ULTIMOVALOR":
            case "ÚLTIMO VALOR":
                estatistica = EstatisticaAlvo.UltimoValor; return true;
            case "VAR":
            case "VARIACAO":
            case "VARIAÇÃO":
                estatistica = EstatisticaAlvo.Variacao; return true;
            default:
                estatistica = null;
                return false;
        }
    }
}
