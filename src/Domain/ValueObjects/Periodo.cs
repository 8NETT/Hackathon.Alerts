namespace Domain.ValueObjects;

public sealed record Periodo
{
    public DateTimeOffset Inicio { get; }
    public DateTimeOffset Fim { get; }

    internal Periodo(DateTimeOffset inicio, DateTimeOffset fim)
    {
        if (inicio >= fim)
            throw new ArgumentException("O início do período deve ser anterior ao fim.");

        Inicio = inicio;
        Fim = fim;
    }

    public TimeSpan Duracao => Fim - Inicio;
    public bool Contem(DateTimeOffset timestamp) => timestamp >= Inicio && timestamp < Fim;
}