using Application.DTOs;
using Application.Mapping;
using Application.Persistence;

namespace Application.UseCases.Regra.ObterRegrasAtivas;

public sealed class ObterRegrasAtivasUseCase : BaseUseCase<object?, IEnumerable<RegraDTO>>, IObterRegrasAtivasUseCase
{
    public ObterRegrasAtivasUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected override async Task<Result<IEnumerable<RegraDTO>>> ExecuteCoreAsync(object? _, CancellationToken cancellation = default)
    {
        var regras = await _unitOfWork.RegraDeAlertaRepository.ObterAtivasAsync(cancellation);
        return Result.Success(regras.Select(r => r.ToDTO()));
    }
}
