using Application.DTOs;
using Domain.ValueObjects;

namespace Application.Mapping;

internal static class PeriodoMapper
{
    public static PeriodoDTO ToDTO(this Periodo periodo) => new()
    {
        Inicio = periodo.Inicio,
        Fim = periodo.Fim
    };
}
