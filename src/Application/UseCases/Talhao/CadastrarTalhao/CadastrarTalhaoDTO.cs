namespace Application.UseCases.Talhao.CadastrarTalhao;

public sealed record CadastrarTalhaoDTO
{
    public required Guid Id { get; init; }
    public required Guid ProprietarioId { get; init; }
}
