using Domain.Parsing;

namespace Domain.ValueObjects;

public sealed record EstatisticaAlvo : Enumeration
{
    private EstatisticaAlvo() : base() { }

    private EstatisticaAlvo(string codigo) : base(codigo) { }

    public static readonly EstatisticaAlvo Minima = new("min");
    public static readonly EstatisticaAlvo Maxima = new("max");
    public static readonly EstatisticaAlvo Media = new("med");
    public static readonly EstatisticaAlvo Soma = new("sum");
    public static readonly EstatisticaAlvo UltimoValor = new("last");
    public static readonly EstatisticaAlvo Variacao = new("var");

    public static EstatisticaAlvo Parse(string? value) => 
        EstatisticaAlvoParser.Parse(value);

    public static bool TryParse(string? value, out EstatisticaAlvo? estatisticaAlvo) =>
        EstatisticaAlvoParser.TryParse(value, out estatisticaAlvo);

    public override string ToString() =>
        Codigo switch
        {
            "min" => "Mínima",
            "max" => "Máxima",
            "med" => "Média",
            "sum" => "Soma",
            "last" => "Último Valor",
            "var" => "Variação",
            _ => throw new InvalidOperationException("Estatística Alvo inválida.")
        };
}
