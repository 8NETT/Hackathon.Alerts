using Domain.Parsing;

namespace Domain.ValueObjects;

public sealed record TipoSensor : Enumeration
{
    private TipoSensor() : base() { }

    private TipoSensor(string codigo) : base(codigo) { }

    public static readonly TipoSensor Temperatura = new("T");
    public static readonly TipoSensor Umidade = new("U");
    public static readonly TipoSensor Precipitacao = new("P");

    public static TipoSensor Parse(string tipo) => 
        TipoSensorParser.Parse(tipo);

    public static bool TryParse(string? value, out TipoSensor? tipo) =>
        TipoSensorParser.TryParse(value, out tipo);

    public override string ToString() =>
        Codigo switch
        {
            "T" => "Temperatura",
            "U" => "Umidade",
            "P" => "Precipitação",
            _ => throw new InvalidOperationException("Tipo de sensor inválido.")
        };
}