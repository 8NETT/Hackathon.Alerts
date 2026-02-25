using Application.DTOs;
using Application.UseCases.Regra.CadastrarRegra;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Mapping;

internal static class RegraMapper
{
    public static RegraDeAlerta ToEntity(this CadastrarRegraDTO dto) => new()
    {
        Tipo = TipoSensor.Parse(dto.Tipo),
        Alvo = EstatisticaAlvo.Parse(dto.Alvo),
        Operador = OperadorComparacao.Parse(dto.Operador),
        Limite = dto.Limite,
        JanelasConsecutivas = dto.JanelasConsecutivas,
        ExigirJanelaCompleta = dto.ExigirJanelaCompleta,
        Nome = dto.Nome,
        Severidade = Severidade.Parse(dto.Severidade),
        Ativa = true
    };

    public static RegraDTO ToDTO(this RegraDeAlerta regra) => new()
    {
        Id = regra.Id,
        Tipo = regra.Tipo.ToString(),
        Alvo = regra.Alvo.ToString(),
        Operador = regra.Operador.ToString(),
        Limite = regra.Limite,
        JanelasConsecutivas = regra.JanelasConsecutivas,
        ExigirJanelaCompleta = regra.ExigirJanelaCompleta,
        Nome = regra.Nome,
        Severidade = regra.Severidade.ToString(),
        Ativa = regra.Ativa
    };
}
