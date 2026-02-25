using Application.DTOs;
using Application.UseCases.Leitura.CadastrarLeitura;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Mapping;

internal static class LeituraMapper
{
    public static Leitura ToValueObject(this CadastrarLeituraDTO dto) => new()
    {
        TalhaoId = dto.TalhaoId,
        Tipo = TipoSensor.Parse(dto.Tipo),
        Valor = dto.Valor,
        Timestamp = dto.Timestamp
    };

    public static LeituraAgregadaDTO ToDTO(this LeituraAgregada leitura) => new()
    {
        TalhaoId = leitura.TalhaoId,
        Tipo = leitura.Tipo.ToString(),
        Janela = leitura.Janela.ToDTO(),
        Estatisticas = leitura.Estatisticas.ToDTO(),
        PrimeiroTimestamp = leitura.PrimeiroTimestamp,
        UltimoTimestamp = leitura.UltimoTimestamp,
        UltimoValor = leitura.UltimoValor,
        JanelaCompleta = leitura.JanelaCompleta
    };
}
