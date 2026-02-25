namespace Application.DTOs;

public sealed record EstatisticasAgregadasDTO
{
    public required double Minima { get; init; }
    public required double Maxima { get; init; }
    public required double Media { get; init; }
    public required double Soma { get; init; }
    public required int Quantidade { get; init; }
    public required double Variacao { get; init; }
}
