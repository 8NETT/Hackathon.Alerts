namespace Infrastructure.Messaging.Events;

internal sealed record TalhaoRemovidoEvent
{
    public required Guid TalhaoId { get; init; }
}
