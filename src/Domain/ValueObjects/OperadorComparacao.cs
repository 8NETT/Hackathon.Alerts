using Domain.Parsing;

namespace Domain.ValueObjects;

public sealed record OperadorComparacao : Enumeration
{

    private OperadorComparacao() : base() { }

    private OperadorComparacao(string codigo) : base(codigo) { }

    public static readonly OperadorComparacao MaiorQue = new(">");
    public static readonly OperadorComparacao MaiorOuIgual = new(">=");
    public static readonly OperadorComparacao MenorQue = new("<");
    public static readonly OperadorComparacao MenorOuIgual = new("<=");
    public static readonly OperadorComparacao Igual = new("=");

    public static OperadorComparacao Parse(string? value) =>
        OperadorComparacaoParser.Parse(value);

    public static bool TryParse(string? value, out OperadorComparacao? operador) =>
        OperadorComparacaoParser.TryParse(value, out operador);

    public bool Compara(double valor, double limite) =>
        Codigo switch
        {
            ">" => valor > limite,
            ">=" => valor >= limite,
            "<" => valor < limite,
            "<=" => valor <= limite,
            "=" => Math.Abs(valor - limite) < 0.000001,
            _ => throw new InvalidOperationException("Operador de comparação inválido.")
        };
}
