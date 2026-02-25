using Application.DTOs;
using Domain.ValueObjects;

namespace Application.Mapping;

internal static class EstatisticasMapper
{
    public static EstatisticasAgregadasDTO ToDTO(this EstatisticasAgregadas estatisticas) => new()
    {
        Minima = estatisticas.Minima,
        Maxima = estatisticas.Maxima,
        Media = estatisticas.Media,
        Soma = estatisticas.Soma,
        Quantidade = estatisticas.Quantidade,
        Variacao = estatisticas.Variacao
    };
}
