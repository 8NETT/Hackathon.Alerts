using API.Mapping;
using API.Requests;
using API.Security;
using API.Validation;
using Application.UseCases.Leitura.ObterLeitura;
using Application.UseCases.Regra.AtivarRegra;
using Application.UseCases.Regra.DesativarRegra;

namespace API.Endpoints;

internal static class LeituraEndpoint
{
    public static RouteGroupBuilder MapLeituraEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/leitura")
            .WithTags("Leituras")
            .AddEndpointFilter<FluentValidationFilter>()
            .RequireAuthorization();

        group.MapGet("/", async (
            ObterLeituraRequest request,
            IObterLeituraUseCase useCase,
            ICurrentUser user,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(request.ToApplicationDTO(user.Id), ct);

            if (result.IsNotFound())
                return Results.NotFound();
            if (result.IsForbidden())
                return Results.Forbid();

            return Results.Ok(result.Value);
        });

        group.MapPatch("/ativar/{id:guid}", async (
            Guid id,
            IAtivarRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(id, ct);

            if (result.IsNotFound())
                return Results.NotFound();

            return Results.Ok();
        });

        group.MapPatch("/desativar/{id:guid}", async (
            Guid id,
            IDesativarRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(id, ct);

            if (result.IsNotFound())
                return Results.NotFound();

            return Results.Ok();
        });

        return group;
    }
}