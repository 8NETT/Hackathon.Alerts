using Domain.Entities;
using Domain.Messaging;
using Domain.Persistence;
using Domain.ValueObjects;

namespace Domain.Services;

public sealed class AvaliadorDeRegras
{
    private readonly ILeituraAgregadaPersistence _repo;
    private readonly IAlertaPublisher _publisher;
    private readonly IAntiSpamDeAlertas _antiSpam;

    public AvaliadorDeRegras(ILeituraAgregadaPersistence repo, IAlertaPublisher publisher, IAntiSpamDeAlertas antiSpam)
    {
        _repo = repo;
        _publisher = publisher;
        _antiSpam = antiSpam;
    }

    public async Task AvaliarAsync(RegraDeAlerta regra, LeituraAgregada janelaAtual, CancellationToken cancellation = default)
    {
        if (!regra.Ativa)
            return;
        if (regra.Tipo != janelaAtual.Tipo)
            return;

        // Ponto de corte: "até o fim da janela atual"
        var fimExclusivo = janelaAtual.Janela.Fim;

        var janelas = await _repo.ObterUltimasJanelasAsync(
            janelaAtual.TalhaoId,
            janelaAtual.Tipo,
            fimExclusivo,
            regra.JanelasConsecutivas,
            cancellation);

        if (janelas.Count < regra.JanelasConsecutivas)
            return; // não há dados suficientes ainda

        if (regra.ExigirJanelaCompleta && janelas.Any(j => !j.JanelaCompleta))
            return;

        if (!SaoJanelasConsecutivas(janelas))
            return;

        // Combina estatísticas
        var combinadas = janelas.Select(j => j.Estatisticas).Combinar();

        // Verifica se deve disparar o alarme
        if (!regra.DeveDispararAlerta(janelaAtual, combinadas, out var alerta))
            return;

        // Anti-spam para não disparar sempre
        if (!await _antiSpam.PodeDispararAsync(janelaAtual.TalhaoId, regra.Id, fimExclusivo, cancellation))
            return;

        await _publisher.PublicarAsync(alerta!, cancellation);
        await _antiSpam.RegistrarDisparoAsync(janelaAtual.TalhaoId, regra.Id, fimExclusivo, cancellation);
    }

    private bool SaoJanelasConsecutivas(IReadOnlyList<LeituraAgregada> janelas)
    {
        if (janelas.Count <= 1)
            return true;

        // Garante ordenação crescente
        var ordenadas = janelas
            .OrderBy(j => j.Janela.Inicio)
            .ToList();

        for (int i = 0; i < ordenadas.Count - 1; i++)
        {
            var atual = ordenadas[i];
            var proxima = ordenadas[i + 1];

            if (atual.Janela.Inicio.AddHours(1) != proxima.Janela.Inicio)
                return false;
        }

        return true;
    }
}
