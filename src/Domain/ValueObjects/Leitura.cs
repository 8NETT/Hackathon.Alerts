namespace Domain.ValueObjects;

public sealed record Leitura
{
    public required Guid TalhaoId { get; init; }
    public required TipoSensor Tipo { get; init; }
    public required UnidadeDeMedida Unidade { get; init; }
    public required double Valor { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}
