using Application.UseCases.Talhao.CadastrarTalhao;
using Application.UseCases.Talhao.RemoverTalhao;
using Infrastructure.Messaging.Events;
using Infrastructure.Messaging.Mapping;

namespace Infrastructure.Messaging;

public sealed class TalhaoEventsConsumerHostedService : IHostedService
{
    private readonly EventHubConsumerClient _client;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<TalhaoEventsConsumerHostedService>? _logger;
    private CancellationTokenSource? _cts;

    public TalhaoEventsConsumerHostedService(
        IServiceScopeFactory scopeFactory,
        string consumerGroup,
        string connectionString,
        ILogger<TalhaoEventsConsumerHostedService>? logger = null)
            : this(scopeFactory, new EventHubConsumerClient(consumerGroup, connectionString), logger) { }

    public TalhaoEventsConsumerHostedService(
        IServiceScopeFactory scopeFactory,
        EventHubConsumerClient client,
        ILogger<TalhaoEventsConsumerHostedService>? logger = null)
    {
        _client = client;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        _ = Task.Run(async () =>
        {
            await foreach (var @event in _client.ReadEventsAsync(cancellationToken))
            {
                if (@event.Data == null)
                {
                    _logger?.LogWarning("Dados do evento veio vazio.");
                    continue;
                }

                var json = Encoding.UTF8.GetString(@event.Data.EventBody.ToArray());

                using var doc = JsonDocument.Parse(json);
                var type = doc.RootElement.GetProperty("type").GetString();

                switch (type)
                {
                    case "TalhaoCriado":
                        await HandleTalhaoCriado(json, cancellationToken);
                        break;
                    case "TalhaoRemovido":
                        await HandleTalhaoRemovido(json, cancellationToken);
                        break;
                    case "TalhaoAlterado":
                        break;
                    default:
                        _logger?.LogWarning("Evento desconhecido: {Type}. JSON: {Json}", type, json);
                        break;
                }
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts?.Cancel();
        return Task.CompletedTask;
    }

    private async Task HandleTalhaoCriado(string json, CancellationToken cancellationToken)
    {
        TalhaoCriadoEvent @event;

        try
        {
            @event = JsonSerializer.Deserialize<TalhaoCriadoEvent>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new JsonException("Payload inválido (null).");
        }
        catch (JsonException ex)
        {
            _logger?.LogError(ex, "Erro ao desserializar evento TalhaoCriado. JSON: {Json}", json);
            throw;
        }

        var dto = @event.ToDTO();

        using var scope = _scopeFactory.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<ICadastrarTalhaoUseCase>();
        var result = await useCase.HandleAsync(dto, cancellationToken);

        if (!result.IsSuccess)
            _logger?.LogError("Erro ao processar o evento TalhaoCriado. Erros: {Errors}. JSON: {Json}", string.Join(", ", result.Errors), json);
        else
            _logger?.LogInformation("Evento TalhaoCriado processado com sucesso. TalhaoId: {TalhaoId}. JSON: {Json}", @event.TalhaoId, json);
    }

    private async Task HandleTalhaoRemovido(string json, CancellationToken cancellationToken)
    {
        TalhaoRemovidoEvent @event;

        try
        {
            @event = JsonSerializer.Deserialize<TalhaoRemovidoEvent>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new JsonException("Payload inválido (null).");
        }
        catch (JsonException ex)
        {
            _logger?.LogError(ex, "Erro ao desserializar evento TalhaoCriado. JSON: {Json}", json);
            throw;
        }

        using var scope = _scopeFactory.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<IRemoverTalhaoUseCase>();
        var result = await useCase.HandleAsync(@event.TalhaoId, cancellationToken);

        if (!result.IsSuccess)
            _logger?.LogError("Erro ao processar o evento TalhaoCriado. Erros: {Errors}. JSON: {Json}", string.Join(", ", result.Errors), json);
        else
            _logger?.LogInformation("Evento TalhaoCriado processado com sucesso. TalhaoId: {TalhaoId}. JSON: {Json}", @event.TalhaoId, json);
    }
}
