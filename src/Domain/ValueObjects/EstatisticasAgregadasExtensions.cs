namespace Domain.ValueObjects;

internal static class EstatisticasAgregadasExtensions
{
    public static EstatisticasAgregadas Combinar(this IEnumerable<EstatisticasAgregadas> estatisticasAgregadas) =>
        EstatisticasAgregadas.Combinar(estatisticasAgregadas);
}
