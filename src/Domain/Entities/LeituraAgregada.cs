using Domain.ValueObjects;

namespace Domain.Entities;

public sealed class LeituraAgregada : BaseEntity
{
    private const int ToleranciaEmMinutos = 10;

    public Guid TalhaoId { get; private set; }
    public TipoSensor Tipo { get; private set; }
    public UnidadeDeMedida Unidade { get; private set; }
    public Periodo Janela { get; private set; }
    public EstatisticasAgregadas Estatisticas { get; private set; }
    public DateTimeOffset PrimeiroTimestamp { get; private set; }
    public DateTimeOffset UltimoTimestamp { get; private set; }
    public double UltimoValor { get; private set; }
    public bool JanelaCompleta => 
        PrimeiroTimestamp <= Janela.Inicio.AddMinutes(ToleranciaEmMinutos) &&
        UltimoTimestamp >= Janela.Fim.AddMinutes(-ToleranciaEmMinutos);

    public LeituraAgregada(Leitura leitura)
    {
        if (leitura is null)
            throw new ArgumentNullException("Leitura não pode ser vazia.", nameof(leitura));
        
        var inicio = new DateTimeOffset(leitura.Timestamp.Year, leitura.Timestamp.Month, leitura.Timestamp.Day, leitura.Timestamp.Hour, 0, 0, leitura.Timestamp.Offset);
        var fim = inicio.AddHours(1);

        Id = Guid.NewGuid();
        TalhaoId = leitura.TalhaoId;
        Tipo = leitura.Tipo;
        Unidade = leitura.Unidade;
        Janela = new Periodo(inicio, fim);
        Estatisticas = new EstatisticasAgregadas(leitura.Valor);
        PrimeiroTimestamp = leitura.Timestamp;
        UltimoTimestamp = leitura.Timestamp;
        UltimoValor = leitura.Valor;
    }

    public void Agregar(Leitura leitura)
    {
        if (leitura is null)
            throw new ArgumentNullException(nameof(leitura));
        if (leitura.TalhaoId != TalhaoId)
            throw new InvalidOperationException("Talhão incompatível para agregação.");
        if (leitura.Tipo != Tipo)
            throw new InvalidOperationException("Tipo incompatível para agregação.");
        if (!Janela.Contem(leitura.Timestamp))
            throw new InvalidOperationException("Leitura fora do período de agregação.");
        if (leitura.Unidade != Unidade)
            throw new InvalidOperationException("Leitura com unidade divergente.");
        
        Estatisticas = Estatisticas.Agregar(leitura.Valor);
        PrimeiroTimestamp = PrimeiroTimestamp < leitura.Timestamp ? PrimeiroTimestamp : leitura.Timestamp;
        
        if (leitura.Timestamp >= UltimoTimestamp)
        {
            UltimoTimestamp = leitura.Timestamp;
            UltimoValor = leitura.Valor;
        }
    }
}