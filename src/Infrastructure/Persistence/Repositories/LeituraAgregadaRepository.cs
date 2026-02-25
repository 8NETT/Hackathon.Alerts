using Application.Persistence;
using Domain.Entities;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repositories;

public sealed class LeituraAgregadaRepository : Repository<LeituraAgregada>, ILeituraAgregadaRepository
{
    public LeituraAgregadaRepository(ApplicationDbContext context) : base(context) { }

    public async Task<LeituraAgregada?> ObterDoHorario(
        Guid talhaoId, 
        TipoSensor tipo, 
        DateTimeOffset timestamp, 
        CancellationToken cancellation = default)
    {
        var inicio = new DateTimeOffset(timestamp.Year, timestamp.Month, timestamp.Day, timestamp.Hour, 0, 0, timestamp.Offset);
        return await _dbSet
            .SingleOrDefaultAsync(l => 
                l.TalhaoId == talhaoId && 
                l.Tipo == tipo && 
                l.Janela.Inicio == inicio, 
                cancellation);
    }

    public async Task<IReadOnlyList<LeituraAgregada>> ObterUltimasJanelasAsync(
        Guid talhaoId, 
        TipoSensor tipo, 
        DateTimeOffset fimExclusivo, 
        int quantidade, 
        CancellationToken cancellation = default)
    {
        // Normaliza o fimExclusivo para a borda da hora
        var fimNormalizado = new DateTimeOffset(
            fimExclusivo.Year, fimExclusivo.Month, fimExclusivo.Day,
            fimExclusivo.Hour, 0, 0,
            fimExclusivo.Offset);

        // Busca as últimas N janelas antes de fimNormalizado
        var ultimas = await _dbSet
            .AsNoTracking()
            .Where(l =>
                l.TalhaoId == talhaoId &&
                l.Tipo == tipo &&
                l.Janela.Inicio < fimNormalizado)
            .OrderByDescending(l => l.Janela.Inicio)
            .Take(quantidade)
            .ToArrayAsync(cancellation);

        // Retorna em ordem crescente (mais antigo -> mais recente),
        // o que facilita a validação de consecutividade no domínio.
        ultimas.Reverse();
        return ultimas;
    }
}
