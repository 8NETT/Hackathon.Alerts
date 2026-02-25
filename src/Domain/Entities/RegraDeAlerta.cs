using Domain.Builders;
using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class RegraDeAlerta : BaseEntity
{
    public TipoSensor Tipo { get; internal set; }
    public EstatisticaAlvo Alvo { get; internal set; }
    public OperadorComparacao Operador { get; internal set; }
    public double Limite { get; internal set; }
    public int JanelasConsecutivas { get; internal set; }
    public bool ExigirJanelaCompleta { get; internal set; }
    public string Nome { get; internal set; }
    public Severidade Severidade { get; internal set; }
    public bool Ativa { get; internal set; }

    internal RegraDeAlerta() { Tipo = null!; Alvo = null!; Operador = null!; Nome = null!; Severidade = null!; }

    public static RegraDeAlertaBuilder Nova => new();

    public void Ativar()
    {
        Ativa = true;
    }

    public void Desativar()
    {
        Ativa = false;
    }

    internal bool DeveDispararAlerta(LeituraAgregada janelaAtual, EstatisticasAgregadas estatisticas, out AlertaDisparado? alerta)
    {
        var valor = ObterValorDeComparacao(janelaAtual, estatisticas);
        var disparou = Operador.Compara(valor, Limite);

        if (disparou)
            alerta = new AlertaDisparado(janelaAtual, this, valor);
        else
            alerta = null;

        return disparou;
    }

    private double ObterValorDeComparacao(LeituraAgregada janelaAtual, EstatisticasAgregadas estatisticas)
    {
        if (Alvo == EstatisticaAlvo.Minima) return estatisticas.Minima;
        if (Alvo == EstatisticaAlvo.Maxima) return estatisticas.Maxima;
        if (Alvo == EstatisticaAlvo.Media) return estatisticas.Media;
        if (Alvo == EstatisticaAlvo.Soma) return estatisticas.Soma;
        if (Alvo == EstatisticaAlvo.UltimoValor) return janelaAtual.UltimoValor;

        throw new InvalidOperationException($"Alvo desconhecido: {Alvo}");
    }
}