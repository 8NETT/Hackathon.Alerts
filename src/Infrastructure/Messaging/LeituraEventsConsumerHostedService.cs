using Application.UseCases.Leitura.CadastrarLeitura;
using Application.UseCases.Talhao.CadastrarTalhao;
using Application.UseCases.Talhao.RemoverTalhao;
using Infrastructure.Messaging.Events;
using Infrastructure.Messaging.Mapping;

namespace Infrastructure.Messaging;

public sealed class LeituraEventsConsumerHostedService : IHostedService
{
    private readonly EventHubConsumerClient _client;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<LeituraEventsConsumerHostedService>? _logger;
    private CancellationTokenSource? _cts;

    public LeituraEventsConsumerHostedService(
        IServiceScopeFactory scopeFactory,
        string consumerGroup,
        string connectionString,
        ILogger<LeituraEventsConsumerHostedService>? logger = null)
            : this(scopeFactory, new EventHubConsumerClient(consumerGroup, connectionString), logger) { }

    public LeituraEventsConsumerHostedService(
        IServiceScopeFactory scopeFactory,
        EventHubConsumerClient client,
        ILogger<LeituraEventsConsumerHostedService>? logger = null)
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
                    case "LeituraCadastrada":
                        await HandleLeituraCadastrada(json, cancellationToken);
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

    private async Task HandleLeituraCadastrada(string json, CancellationToken cancellationToken)
    {
        LeituraCadastrada @event;

        try
        {
            @event = JsonSerializer.Deserialize<LeituraCadastrada>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new JsonException("Payload inválido (null).");
        }
        catch (JsonException ex)
        {
            _logger?.LogError(ex, "Erro ao desserializar evento LeituraCadastrada. JSON: {Json}", json);
            throw;
        }

        var dto = @event.ToDTO();

        using var scope = _scopeFactory.CreateScope();
        var useCase = scope.ServiceProvider.GetRequiredService<ICadastrarLeituraUseCase>();
        var result = await useCase.HandleAsync(dto, cancellationToken);

        if (!result.IsSuccess)
            _logger?.LogError("Erro ao processar o evento LeituraCadastrada. Erros: {Errors}. JSON: {Json}", string.Join(", ", result.Errors), json);
        else
            _logger?.LogInformation("Evento LeituraCadastrada processado com sucesso. TalhaoId: {TalhaoId}. SensorId: {SensorId}. JSON: {Json}", @event.TalhaoId, @event.SensorId, json);
    }
}
