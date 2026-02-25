using API.Requests;
using Application.UseCases.Leitura.ObterLeitura;

namespace API.Mapping;

internal static class LeituraMapper
{
    public static ObterLeituraDTO ToApplicationDTO(this ObterLeituraRequest request, Guid usuarioId) => new()
    {
        TalhaoId = request.TalhaoId!.Value,
        UsuarioId = usuarioId,
        Tipo = request.Tipo!,
        Timestamp = request.Timestamp!.Value
    };
}
