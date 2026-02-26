using Application.Mapping;
using Application.Persistence;

namespace Application.UseCases.Regra.RemoverRegra;

public sealed class RemoverRegraUseCase : BaseUseCase<Guid>, IRemoverRegraUseCase
{
    public RemoverRegraUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected override async Task<Result<object>> ExecuteCoreAsync(Guid id, CancellationToken cancellation = default)
    {
        var regra = await _unitOfWork.RegraDeAlertaRepository.ObterAsync(id, cancellation);

        if (regra is null)
            return Result.NotFound($"Regra com ID {id} não localizada.");

        _unitOfWork.RegraDeAlertaRepository.Remover(regra);
        await _unitOfWork.SalvarAsync(cancellation);

        return Result.Success();
    }
}