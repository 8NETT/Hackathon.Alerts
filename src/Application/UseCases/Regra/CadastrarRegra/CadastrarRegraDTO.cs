namespace Application.UseCases.Regra.CadastrarRegra;

public sealed record CadastrarRegraDTO
{
    public required string Tipo { get; init; }
    public required string Alvo { get; init; }
    public required string Operador { get; init; }
    public required double Limite { get; init; }
    public required int JanelasConsecutivas { get; init; }
    public required bool ExigirJanelaCompleta { get; init; }
    public required string Nome { get; init; }
    public required string Severidade { get; init; }
}