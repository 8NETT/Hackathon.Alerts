using Domain.Messaging;

namespace Infrastructure.Messaging;

public sealed class AntiSpamDeAlertasEmMemoria : IAntiSpamDeAlertas
{
    private readonly ConcurrentDictionary<(Guid talhaoId, Guid regraId), DateTimeOffset> _ultimoDisparo
        = new();

    private readonly TimeSpan _retencao;

    public AntiSpamDeAlertasEmMemoria(TimeSpan? retencao = null)
    {
        _retencao = retencao ?? TimeSpan.FromDays(7);
    }

    public Task<bool> PodeDispararAsync(
        Guid talhaoId,
        Guid regraId,
        DateTimeOffset referencia,
        CancellationToken cancellation = default)
    {
        cancellation.ThrowIfCancellationRequested();

        var key = (talhaoId, regraId);

        // Se nunca disparou, pode disparar
        if (!_ultimoDisparo.TryGetValue(key, out var ultimaReferencia))
            return Task.FromResult(true);

        // Dedup por referência:
        // se já disparou exatamente para essa referência, não pode
        var pode = ultimaReferencia != referencia;
        return Task.FromResult(pode);
    }

    public Task RegistrarDisparoAsync(
        Guid talhaoId,
        Guid regraId,
        DateTimeOffset referencia,
        CancellationToken cancellation = default)
    {
        cancellation.ThrowIfCancellationRequested();

        var key = (talhaoId, regraId);
        _ultimoDisparo[key] = referencia;

        LimpezaOcasional(referencia);
        return Task.CompletedTask;
    }

    // Limpeza simples para não crescer indefinidamente
    // Remove entradas muito antigas comparando com a referência atual.
    private void LimpezaOcasional(DateTimeOffset referenciaAtual)
    {
        // Limpeza "barata": roda em 1% das chamadas aproximadamente
        // (evita custo em toda gravação)
        if ((referenciaAtual.Ticks & 0xFF) != 0) return;

        var limite = referenciaAtual - _retencao;

        foreach (var kv in _ultimoDisparo)
        {
            if (kv.Value < limite)
                _ultimoDisparo.TryRemove(kv.Key, out _);
        }
    }
}

