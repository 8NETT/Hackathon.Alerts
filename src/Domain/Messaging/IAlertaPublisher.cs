using Domain.ValueObjects;

namespace Domain.Messaging;

public interface IAlertaPublisher
{
    Task PublicarAsync(AlertaDisparado alerta, CancellationToken cancellation = default);
}
