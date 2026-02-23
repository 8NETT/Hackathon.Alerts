namespace Domain.ValueObjects;

public sealed record Severidade : Enumeration
{
    private Severidade() : base() { }

    public Severidade(string codigo) : base(codigo) { }

    public static readonly Severidade Alta = new("A");
    public static readonly Severidade Media = new("M");
    public static readonly Severidade Baixa = new("B");

    public override string ToString() =>
        Codigo switch
        {
            "A" => "Alta",
            "M" => "Média",
            "B" => "Baixa",
            _ => throw new InvalidOperationException("Severidade inválida.")
        };
}