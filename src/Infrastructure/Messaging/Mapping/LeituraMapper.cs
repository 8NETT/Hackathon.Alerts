using Application.UseCases.Leitura.CadastrarLeitura;
using Infrastructure.Messaging.Events;

namespace Infrastructure.Messaging.Mapping;

internal static class LeituraMapper
{
    public static CadastrarLeituraDTO ToDTO(this LeituraCadastrada @event) =>
        new()
        {
            TalhaoId = @event.TalhaoId,
            Tipo = @event.Tipo,
            Unidade = @event.Unidade,
            Valor = @event.Valor,
            Timestamp = @event.Timestamp
        };
}
