namespace Domain.ValueObjects;

public sealed record TipoSensor : IEquatable<TipoSensor>
{
    public string Codigo { get; }

    private TipoSensor() { Codigo = null!; }

    private TipoSensor(string codigo)
    {
        Codigo = codigo;
    }

    public static readonly TipoSensor Temperatura = new("T");
    public static readonly TipoSensor Umidade = new("U");
    public static readonly TipoSensor Precipitacao = new("P");

    public override string ToString() =>
        Codigo switch
        {
            "T" => "Temperatura",
            "U" => "Umidade",
            "P" => "Precipitação",
            _ => throw new InvalidOperationException("Tipo de sensor inválido.")
        };
}