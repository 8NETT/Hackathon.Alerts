namespace Domain.Messaging;

public interface IAntiSpamDeAlertas
{
    Task<bool> PodeDispararAsync(Guid talhaoId, Guid regraId, DateTimeOffset referencia, CancellationToken cancellation = default);
    Task RegistrarDisparoAsync(Guid talhaoId, Guid regraId, DateTimeOffset referencia, CancellationToken cancellation = default);
}
