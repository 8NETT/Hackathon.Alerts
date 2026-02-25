using Domain.ValueObjects;

namespace Domain.Parsing;

internal static class OperadorComparacaoParser
{
    public static OperadorComparacao Parse(string? value)
    {
        if (TryParse(value, out var operador))
            return operador!;

        throw new ArgumentException("OperadorComparacao inválido.");
    }

    public static bool TryParse(string? value, out OperadorComparacao? operador)
    {
        switch (value?.Trim().ToUpperInvariant())
        {
            case ">":
            case "MAIOR":
            case "MAIORQUE":
            case "MAIOR QUE":
                operador = OperadorComparacao.MaiorQue; return true;
            case ">=":
            case "MAIORIGUAL":
            case "MAIOR IGUAL":
            case "MAIOROUIGUAL":
            case "MAIOR OU IGUAL":
                operador = OperadorComparacao.MaiorOuIgual; return true;
            case "<":
            case "MENOR":
            case "MENORQUE":
            case "MENOR QUE":
                operador = OperadorComparacao.MenorQue; return true;
            case "<=":
            case "MENORIGUAL":
            case "MENOR IGUAL":
            case "MENOROUIGUAL":
            case "MENOR OU IGUAL":
                operador = OperadorComparacao.MenorOuIgual; return true;
            case "=":
            case "==":
            case "===":
            case "IGUAL":
                operador = OperadorComparacao.Igual; return true;
            default:
                operador = null;
                return false;
        }
    }
}
