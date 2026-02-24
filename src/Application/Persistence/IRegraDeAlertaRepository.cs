using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Persistence;

public interface IRegraDeAlertaRepository : IRepository<RegraDeAlerta>
{
    Task<IEnumerable<RegraDeAlerta>> ObterRegrasAtivas(TipoSensor tipo, CancellationToken cancellation);
}
