using Domain.Parsing;

namespace Domain.ValueObjects;

public sealed record Severidade : Enumeration
{
    private Severidade() : base() { }

    public Severidade(string codigo) : base(codigo) { }

    public static readonly Severidade Alta = new("A");
    public static readonly Severidade Media = new("M");
    public static readonly Severidade Baixa = new("B");

    public static Severidade Parse(string? value) =>
        SeveridadeParser.Parse(value);

    public static bool TryParse(string? value, out Severidade severidade) =>
        SeveridadeParser.TryParse(value, out severidade);

    public override string ToString() =>
        Codigo switch
        {
            "A" => "Alta",
            "M" => "Média",
            "B" => "Baixa",
            _ => throw new InvalidOperationException("Severidade inválida.")
        };
}