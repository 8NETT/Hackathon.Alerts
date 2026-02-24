using Domain.ValueObjects;

namespace Domain.Parsing;

public sealed class TipoSensorParser
{
    public static TipoSensor Parse(string value)
    {
        if (TryParse(value, out var tipo))
            return tipo!;

        throw new ArgumentException("Tipo de sensor inválido.");
    }

    public static bool TryParse(string? value, out TipoSensor? tipo)
    {
        switch (value?.Trim().ToUpperInvariant())
        {
            case "T":
            case "TEMPERATURE":
            case "TEMPERATURA":
                tipo = TipoSensor.Temperatura; return true;
            case "U":
            case "HUMIDITY":
            case "UMIDADE":
                tipo = TipoSensor.Umidade; return true;
            case "P":
            case "PRECIPITATION":
            case "PRECIPITAÇÃO":
            case "PRECIPITACAO":
                tipo = TipoSensor.Precipitacao; return true;
            default:
                tipo = null;
                return false;
        }
    }
}
