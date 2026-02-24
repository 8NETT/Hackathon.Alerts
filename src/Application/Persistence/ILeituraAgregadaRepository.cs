using Domain.Entities;
using Domain.Persistence;
using Domain.ValueObjects;

namespace Application.Persistence;

public interface ILeituraAgregadaRepository : IRepository<LeituraAgregada>, ILeituraAgregadaPersistence
{
    Task<LeituraAgregada?> ObterDoHorario(Guid talhaoId, TipoSensor tipo, DateTimeOffset timestamp, CancellationToken cancellation = default);
}
