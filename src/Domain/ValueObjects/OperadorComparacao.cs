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
