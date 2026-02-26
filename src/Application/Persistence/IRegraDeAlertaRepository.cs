using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Persistence;

public interface IRegraDeAlertaRepository : IRepository<RegraDeAlerta>
{
    Task<IEnumerable<RegraDeAlerta>> ObterAtivasAsync(CancellationToken cancellation);
    Task<IEnumerable<RegraDeAlerta>> ObterDoTipoAsync(TipoSensor tipo, CancellationToken cancellation);
}
