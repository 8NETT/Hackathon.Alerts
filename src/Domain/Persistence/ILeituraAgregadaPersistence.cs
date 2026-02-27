using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Persistence;

public interface ILeituraAgregadaPersistence
{
    Task<IReadOnlyList<LeituraAgregada>> ObterUltimasJanelasAsync(
        Guid talhaoId,
        TipoSensor tipo,
        DateTimeOffset fimExclusivo,
        int quantidade,
        CancellationToken cancellation = default);

    Task<IReadOnlyList<LeituraAgregada>> ObterUltimasJanelasCompletasAsync(
        Guid talhaoId,
        TipoSensor tipo,
        DateTimeOffset fimExclusivo,
        int quantidade,
        CancellationToken cancellation = default);
}
