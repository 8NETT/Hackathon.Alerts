using Application.UseCases.Talhao.CadastrarTalhao;
using Infrastructure.Messaging.Events;

namespace Infrastructure.Messaging.Mapping;

internal static class TalhaoMapper
{
    public static CadastrarTalhaoDTO ToDTO(this TalhaoCriadoEvent @event) =>
        new CadastrarTalhaoDTO
        {
            Id = @event.TalhaoId,
            ProprietarioId = @event.ProprietarioId
        };
}
