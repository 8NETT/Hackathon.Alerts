using Application.Persistence;

namespace Application.UseCases.Regra.AtivarRegra;

public sealed class AtivarRegraUseCase : BaseUseCase<Guid>, IAtivarRegraUseCase
{
    public AtivarRegraUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected override async Task<Result<object>> ExecuteCoreAsync(Guid id, CancellationToken cancellation = default)
    {
        var regra = await _unitOfWork.RegraDeAlertaRepository.ObterAsync(id, cancellation);

        if (regra is null)
            return Result.NotFound($"Regra com ID {id} não localizado.");
        if (regra.Ativa)
            return Result.Success();

        regra.Ativar();

        _unitOfWork.RegraDeAlertaRepository.Atualizar(regra);
        await _unitOfWork.SalvarAsync(cancellation);

        return Result.Success();
    }
}
