namespace Infrastructure.Messaging.Events;

internal sealed record TalhaoCriadoEvent
{
    public required Guid TalhaoId { get; init; }
    public required Guid ProprietarioId { get; init; }
}