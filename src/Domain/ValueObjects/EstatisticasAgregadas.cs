namespace Domain.ValueObjects;

public sealed record EstatisticasAgregadas
{
    public double Minima { get; private set; }
    public double Maxima { get; private set; }
    public double Media { get; private set; }
    public double Soma { get; private set; }
    public int Quantidade { get; private set; }
    public double Variacao => Maxima - Minima;

    private EstatisticasAgregadas() { }
    
    internal EstatisticasAgregadas(double valor)
    {
        Minima = valor;
        Maxima = valor;
        Media = valor;
        Soma = valor;
        Quantidade = 1;
    }

    internal EstatisticasAgregadas Agregar(double valor)
    {
        var novaQuantidade = Quantidade + 1;
        var novaSoma = Soma + valor;
        var novaMedia = novaSoma / novaQuantidade;
        var novaMinima = Math.Min(Minima, valor);
        var novaMaxima = Math.Max(Maxima, valor);

        return new EstatisticasAgregadas
        {
            Minima = novaMinima,
            Maxima = novaMaxima,
            Media = novaMedia,
            Soma = novaSoma,
            Quantidade = novaQuantidade
        };
    }

    internal static EstatisticasAgregadas Combinar(IEnumerable<EstatisticasAgregadas> estatisticasAgregadas)
    {
        var lista = estatisticasAgregadas.ToArray();

        if (lista.Length == 0)
            throw new InvalidOperationException("Não é possível combinar uma coleção vazia de estatísticas agregadas.");


        double somaTotal = 0;
        int qtdTotal = 0;
        double min = double.PositiveInfinity;
        double max = double.NegativeInfinity;

        foreach (var estatisticas in lista)
        {
            somaTotal += estatisticas.Soma;
            qtdTotal += estatisticas.Quantidade;
            min = Math.Min(min, estatisticas.Minima);
            max = Math.Max(max, estatisticas.Maxima);
        }

        var media = somaTotal / qtdTotal;

        return new EstatisticasAgregadas
        {
            Minima = min,
            Maxima = max,
            Media = media,
            Soma = somaTotal,
            Quantidade = qtdTotal
        };
    }
}
