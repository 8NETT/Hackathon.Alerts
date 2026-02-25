using Domain.Entities;

namespace Domain.ValueObjects;

public sealed record AlertaDisparado
{
    public Guid Id { get; }
    public Guid TalhaoId { get; }
    public Guid RegraId { get; }
    public TipoSensor Tipo { get; }
    public string NomeRegra { get; }
    public double ValorObservado { get; }
    public DateTimeOffset Timestamp { get; }
    public Severidade Severidade { get; }

    internal AlertaDisparado(LeituraAgregada leitura, RegraDeAlerta regra, double valorObservado)
    {
        Id = Guid.NewGuid();
        TalhaoId = leitura.TalhaoId;
        RegraId = regra.Id;
        Tipo = leitura.Tipo;
        NomeRegra = regra.Nome;
        ValorObservado = valorObservado;
        Timestamp = DateTimeOffset.UtcNow;
        Severidade = regra.Severidade;
    }
}
