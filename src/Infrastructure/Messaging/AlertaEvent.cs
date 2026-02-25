using Domain.ValueObjects;

namespace Infrastructure.Messaging;

internal sealed class AlertaEvent
{
    public Guid Id { get; }
    public string Type { get; }
    public Guid TalhaoId { get; }
    public Guid RegraId { get; }
    public string Tipo { get; }
    public string NomeRegra { get; }
    public double ValorObservado { get; }
    public DateTimeOffset Timestamp { get; }
    public string Severidade { get; }

    public AlertaEvent(AlertaDisparado alerta)
    {
        Id = alerta.Id;
        Type = "AlertaDisparado";
        TalhaoId = alerta.TalhaoId;
        RegraId = alerta.RegraId;
        Tipo = alerta.Tipo.ToString();
        NomeRegra = alerta.NomeRegra;
        ValorObservado = alerta.ValorObservado;
        Timestamp = alerta.Timestamp;
        Severidade = alerta.Severidade.ToString();
    }
}
