using API.Security;
using API.Validation;
using Application.UseCases.Regra.AtivarRegra;
using Application.UseCases.Regra.DesativarRegra;
using Application.UseCases.Regra.ObterRegra;
using Application.UseCases.Regra.ObterRegrasDoTipo;

namespace API.Endpoints;

internal static class RegraEndpoint
{
    public static RouteGroupBuilder MapRegraEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/regra")
            .WithTags("regras")
            .AddEndpointFilter<FluentValidationFilter>()
            .RequireAuthorization();

        group.MapGet("/{id:guid}", async (
            Guid id,
            IObterRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(id, ct);

            if (result.IsNotFound())
                return Results.NotFound();

            return Results.Ok(result.Value);
        });

        group.MapGet("/{tipo:string}", async (
            string tipo,
            IObterRegrasDoTipoUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(tipo, ct);

            if (result.IsInvalid())
                return Results.BadRequest(result.ValidationErrors);

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
