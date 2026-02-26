using Application.DTOs;
using Application.Mapping;
using Application.Persistence;
using Domain.ValueObjects;

namespace Application.UseCases.Regra.ObterRegrasDoTipo;

public sealed class ObterRegrasDoTipoUseCase : BaseUseCase<string, IEnumerable<RegraDTO>>, IObterRegrasDoTipoUseCase
{
    public ObterRegrasDoTipoUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected override async Task<Result<IEnumerable<RegraDTO>>> ExecuteCoreAsync(string input, CancellationToken cancellation = default)
    {
        if (!TipoSensor.TryParse(input, out var tipo))
            return Result.Invalid(new ValidationError("Tipo inválido."));

        var regras = await _unitOfWork.RegraDeAlertaRepository.ObterDoTipo(tipo!, cancellation);

        return Result.Success(regras.Select(r => r.ToDTO()));
    }
}
