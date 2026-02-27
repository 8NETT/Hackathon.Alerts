using API.Requests;
using Application.UseCases.Regra.CadastrarRegra;

namespace API.Mapping;

internal static class RegraMapper
{
    public static CadastrarRegraDTO ToApplicationDTO(this CadastrarRegraRequest request) =>
        new()
        {
            Tipo = request.Tipo!,
            Alvo = request.Alvo!,
            Operador = request.Operador!,
            Limite = request.Limite!.Value,
            JanelasConsecutivas = request.JanelasConsecutivas!.Value,
            ExigirJanelaCompleta = request.ExigirJanelaCompleta!.Value,
            Nome = request.Nome!,
            Severidade = request.Severidade!
        };
}
