namespace Domain.ValueObjects;

public sealed record EstatisticasAgregadas
{
    public double Minima { get; private set; }
    public double Maxima { get; private set; }
    public double Media { get; private set; }
    public double Soma { get; private set; }
    public int Quantidade { get; private set; }

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
}
