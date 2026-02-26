using Application.DTOs;
using Application.Mapping;
using Application.Persistence;

namespace Application.UseCases.Regra.ObterRegra;

public sealed class ObterRegraUseCase : BaseUseCase<Guid, RegraDTO>, IObterRegraUseCase
{
    public ObterRegraUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected override async Task<Result<RegraDTO>> ExecuteCoreAsync(Guid id, CancellationToken cancellation = default)
    {
        var regra = await _unitOfWork.RegraDeAlertaRepository.ObterAsync(id, cancellation);

        if (regra == null)
            return Result.NotFound($"Regra com ID {id} não localizada.");

        return regra.ToDTO();
    }
}
