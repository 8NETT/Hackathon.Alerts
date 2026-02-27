using API.Mapping;
using API.Requests;
using API.Validation;
using Application.UseCases.Regra.AtivarRegra;
using Application.UseCases.Regra.CadastrarRegra;
using Application.UseCases.Regra.DesativarRegra;
using Application.UseCases.Regra.ObterRegra;
using Application.UseCases.Regra.ObterRegrasAtivas;
using Application.UseCases.Regra.ObterRegrasDoTipo;
using Application.UseCases.Regra.RemoverRegra;
using Microsoft.AspNetCore.Mvc;

namespace API.Endpoints;

internal static class RegraEndpoint
{
    public static RouteGroupBuilder MapRegraEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/regra")
            .WithTags("regras")
            .AddEndpointFilter<FluentValidationFilter>()
            .RequireAuthorization();

        group.MapGet("/", async (
            IObterRegrasAtivasUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(ct);
            return Results.Ok(result.Value);
        })
            .WithSummary("Obtém os dados das regras ativas.")
            .Produces(StatusCodes.Status200OK);

        group.MapGet("/{id:guid}", async (
            Guid id,
            IObterRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(id, ct);

            if (result.IsNotFound())
                return Results.NotFound();

            return Results.Ok(result.Value);
        })
            .WithSummary("Obtém os dados de uma regra específica.")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

        group.MapGet("/{tipo}", async (
            string tipo,
            IObterRegrasDoTipoUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(tipo, ct);

            if (result.IsInvalid())
                return Results.BadRequest(result.ValidationErrors);

            return Results.Ok(result.Value);
        })
            .WithSummary("Obtém os dados das regras de um tipo específico.")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK);

        group.MapPost("/", async (
            CadastrarRegraRequest request,
            ICadastrarRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(request.ToApplicationDTO(), ct);

            if (result.IsInvalid())
                return Results.BadRequest(result.ValidationErrors);

            return Results.Ok(result.Value);
        })
            .WithSummary("Cadastra uma nova regra.")
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status200OK)
            .Accepts<CadastrarRegraRequest>("application/json");

        group.MapPatch("/ativar/{id:guid}", async (
            Guid id,
            IAtivarRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(id, ct);

            if (result.IsNotFound())
                return Results.NotFound();

            return Results.Ok();
        })
            .WithSummary("Ativa uma regra específica.")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

        group.MapPatch("/desativar/{id:guid}", async (
            Guid id,
            IDesativarRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(id, ct);

            if (result.IsNotFound())
                return Results.NotFound();

            return Results.Ok();
        })
            .WithSummary("Desativa uma regra específica.")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

        group.MapDelete("/{id:guid}", async (
            Guid id,
            IRemoverRegraUseCase useCase,
            CancellationToken ct) =>
        {
            var result = await useCase.HandleAsync(id, ct);

            if (result.IsNotFound())
                return Results.NotFound();

            return Results.Ok();
        })
            .WithSummary("Remove uma regra específica.")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status200OK);

        return group;
    }
}
