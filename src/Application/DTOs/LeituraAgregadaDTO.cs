namespace Application.DTOs;

public sealed record LeituraAgregadaDTO
{
    public required Guid TalhaoId { get; init; }
    public required string Tipo { get; init; }
    public required string Unidade { get; init; }
    public required PeriodoDTO Janela { get; init; }
    public required EstatisticasAgregadasDTO Estatisticas { get; init; }
    public required DateTimeOffset PrimeiroTimestamp { get; init; }
    public required DateTimeOffset UltimoTimestamp { get; init; }
    public required double UltimoValor { get; init; }
    public required bool JanelaCompleta { get; init; }
}
