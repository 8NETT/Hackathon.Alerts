namespace API.Requests;

public sealed record CadastrarRegraRequest
{
    public string? Tipo { get; init; }
    public string? Alvo { get; init; }
    public string? Operador { get; init; }
    public double? Limite { get; init; }
    public int? JanelasConsecutivas { get; init; }
    public bool? ExigirJanelaCompleta { get; init; }
    public string? Nome { get; init; }
    public string? Severidade { get; init; }
}
