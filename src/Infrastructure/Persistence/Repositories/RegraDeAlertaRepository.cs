using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories;

internal sealed class RegraDeAlertaRepository : Repository<RegraDeAlerta>, IRegraDeAlertaRepository
{
    public RegraDeAlertaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<IEnumerable<RegraDeAlerta>> ObterAtivasAsync(CancellationToken cancellation) =>
        await _dbSet
            .AsNoTracking()
            .Where(r => r.Ativa)
            .ToArrayAsync(cancellation);

    public async Task<IEnumerable<RegraDeAlerta>> ObterAtivasDoTipoAsync(TipoSensor tipo, CancellationToken cancellation) =>
        await _dbSet
            .AsNoTracking()
            .Where(r => r.Ativa && r.Tipo.Codigo == tipo.Codigo)
            .ToArrayAsync(cancellation);

    public async Task<IEnumerable<RegraDeAlerta>> ObterDoTipoAsync(TipoSensor tipo, CancellationToken cancellation) =>
        await _dbSet
            .AsNoTracking()
            .Where(r => r.Tipo.Codigo == tipo.Codigo)
            .ToArrayAsync(cancellation);
}
