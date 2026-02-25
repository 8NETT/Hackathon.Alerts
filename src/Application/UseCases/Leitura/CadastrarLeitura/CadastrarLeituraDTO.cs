namespace Application.UseCases.Leitura.CadastrarLeitura;

public sealed record CadastrarLeituraDTO
{
    public required Guid TalhaoId { get; init; }
    public required string Tipo { get; init; }
    public required string Unidade { get; init; }
    public required double Valor { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}   
