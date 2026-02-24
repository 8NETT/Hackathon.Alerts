using Application.UseCases.Leitura.CadastrarLeitura;
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
}
