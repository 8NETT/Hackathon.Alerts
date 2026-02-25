using Application.UseCases.Talhao.CadastrarTalhao;
using Domain.Entities;

namespace Application.Mapping;

internal static class TalhaoMapper
{
    public static Talhao ToEntity(this CadastrarTalhaoDTO dto) => 
        new(dto.Id, dto.ProprietarioId);
}
