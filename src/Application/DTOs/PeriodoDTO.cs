namespace Application.DTOs;

public sealed record PeriodoDTO
{
    public required DateTimeOffset Inicio { get; init; }
    public required DateTimeOffset Fim { get; init; }
}
