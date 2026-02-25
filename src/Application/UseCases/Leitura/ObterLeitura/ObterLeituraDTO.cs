namespace Application.UseCases.Leitura.ObterLeitura;

public sealed record ObterLeituraDTO
{
    public required Guid TalhaoId { get; init; }
    public required Guid UsuarioId { get; init; }
    public required string Tipo { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}
