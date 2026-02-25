using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories;

internal sealed class RegraDeAlertaRepository : Repository<RegraDeAlerta>, IRegraDeAlertaRepository
{
    public RegraDeAlertaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<RegraDeAlerta>> ObterRegrasAtivas(TipoSensor tipo, CancellationToken cancellation) =>
        await _dbSet
            .AsNoTracking()
            .Where(r => r.Tipo == tipo && r.Ativa)
            .ToArrayAsync(cancellation);
}
