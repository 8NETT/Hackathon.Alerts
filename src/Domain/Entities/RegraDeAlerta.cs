using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class RegraDeAlerta : BaseEntity
{
    public required TipoSensor Tipo { get; init; }
    public required UnidadeDeMedida Unidade { get; init; }
    public required EstatisticaAlvo Alvo { get; init; }
    public required OperadorComparacao Operador { get; init; }
    public required double Limite { get; init; }
    public required int JanelasConsecutivas { get; init; }
    public required bool ExigirJanelaCompleta { get; init; }
    public required string Nome { get; init; }
    public required Severidade Severidade { get; init; }
    public required bool Ativa { get; init; }
}
