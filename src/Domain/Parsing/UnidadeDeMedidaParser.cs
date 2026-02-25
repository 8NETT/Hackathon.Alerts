using Domain.ValueObjects;

namespace Domain.Parsing;

internal static class UnidadeDeMedidaParser
{
    public static UnidadeDeMedida Parse(string value)
    {
        if (TryParse(value, out var unidade))
            return unidade!;

        throw new ArgumentException("Tipo de unidade inválida.");
    }

    public static bool TryParse(string value, out UnidadeDeMedida? unidade)
    {
        switch (value?.Trim().ToUpperInvariant())
        {
            case "°C":
            case "C":
            case "CELSIUS":
                unidade = UnidadeDeMedida.Celsius; return true;
            case "F":
            case "FAHRENHEIT":
                unidade = UnidadeDeMedida.Fahrenheit; return true;
            case "K":
                unidade = UnidadeDeMedida.Kelvin; return true;
            case "%":
                unidade = UnidadeDeMedida.UmidadeRelativa; return true;
            case "MM":
            case "L/M²":
            case "L/M2":
                unidade = UnidadeDeMedida.Milimetros; return true;
            case "IN":
            case "POL":
                unidade = UnidadeDeMedida.Polegadas; return true;
            default:
                unidade = null;
                return false;
        }
    }
}
