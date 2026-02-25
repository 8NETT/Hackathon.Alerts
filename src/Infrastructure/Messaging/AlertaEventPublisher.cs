using Domain.Messaging;
using Domain.ValueObjects;

namespace Infrastructure.Messaging;

public sealed class AlertaEventPublisher : IAlertaPublisher
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly EventHubProducerClient _producer;
    private readonly ILogger<AlertaEventPublisher> _logger;

    public AlertaEventPublisher(EventHubProducerClient producer, ILogger<AlertaEventPublisher> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    public async Task PublicarAsync(AlertaDisparado alerta, CancellationToken cancellation = default)
    {
        // Serializa o evento
        var @event = new AlertaEvent(alerta);
        var json = JsonSerializer.Serialize(alerta, @event.GetType(), JsonOptions);
        var bytes = Encoding.UTF8.GetBytes(json);
        var eventData = new EventData(bytes);

        // Metadados úteis para roteamento/observabilidade
        eventData.Properties["type"] = @event.Type;
        eventData.Properties["eventId"] = @event.Id.ToString();
        eventData.ContentType = "application/json";

        // Envia o evento
        await _producer.SendAsync([eventData], cancellation);
        _logger.LogInformation("Publicado evento {EventType} com ID {EventId}", @event.Type, @event.Id);
    }
}
