using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Persistence;

public interface ILeituraAgregadaRepository
{
    Task<IReadOnlyList<LeituraAgregada>> ObterUltimasJanelasAsync(
        Guid talhaoId,
        TipoSensor tipo,
        DateTimeOffset fimExclusivo,
        int quantidade,
        CancellationToken cancellation = default);
}
