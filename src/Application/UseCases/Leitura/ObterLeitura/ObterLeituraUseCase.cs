using Application.DTOs;
using Application.Mapping;
using Application.Persistence;
using Domain.ValueObjects;

namespace Application.UseCases.Leitura.ObterLeitura;

public sealed class ObterLeituraUseCase : BaseUseCase<ObterLeituraDTO, LeituraAgregadaDTO>, IObterLeituraUseCase
{
    public ObterLeituraUseCase(IUnitOfWork unitOfWork, IValidator<ObterLeituraDTO>? validator, ILoggerFactory loggerFactory) 
        : base(unitOfWork, validator, loggerFactory) { }

    protected override async Task<Result<LeituraAgregadaDTO>> ExecuteCoreAsync(ObterLeituraDTO dto, CancellationToken cancellation = default)
    {
        var tipo = TipoSensor.Parse(dto.Tipo);
        var leitura = await _unitOfWork.LeituraAgregadaRepository.ObterDoHorario(dto.TalhaoId, tipo, dto.Timestamp);

        if (leitura is null)
            return Result.NotFound("Não foi localizado leituras no horário.");

        return leitura.ToDTO();
    }
}
