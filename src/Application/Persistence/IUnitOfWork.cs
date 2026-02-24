namespace Application.Persistence;

public interface IUnitOfWork : IDisposable
{
    ILeituraAgregadaRepository LeituraAgregadaRepository { get; }
    IRegraDeAlertaRepository RegraDeAlertaRepository { get; }
    ITalhaoRepository TalhaoRepository { get; }

    Task SalvarAsync(CancellationToken cancellation = default);
}
