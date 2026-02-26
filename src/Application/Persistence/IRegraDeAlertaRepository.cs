using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Persistence;

public interface IRegraDeAlertaRepository : IRepository<RegraDeAlerta>
{
    Task<IEnumerable<RegraDeAlerta>> ObterAtivas(CancellationToken cancellation);
    Task<IEnumerable<RegraDeAlerta>> ObterDoTipo(TipoSensor tipo, CancellationToken cancellation);
}
