using Application.Persistence;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _context;
    private ITalhaoRepository _talhaoRepository = null!;
    private ILeituraAgregadaRepository _leituraAgregadaRepository = null!;
    private IRegraDeAlertaRepository _regraDeAlertaRepository = null!;
    
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ITalhaoRepository TalhaoRepository =>
        _talhaoRepository = _talhaoRepository ?? new TalhaoRepository(_context);

    public ILeituraAgregadaRepository LeituraAgregadaRepository =>
        _leituraAgregadaRepository = _leituraAgregadaRepository ?? new LeituraAgregadaRepository(_context);

    public IRegraDeAlertaRepository RegraDeAlertaRepository =>
        _regraDeAlertaRepository = _regraDeAlertaRepository ?? new RegraDeAlertaRepository(_context);

    public async Task SalvarAsync(CancellationToken cancellation = default) =>
        await _context.SaveChangesAsync(cancellation);

    public void Dispose() => _context.Dispose();
}
