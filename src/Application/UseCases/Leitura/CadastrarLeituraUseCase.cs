using Application.Persistence;
using Domain.Parsing;

namespace Application.UseCases.Leitura;

public sealed class CadastrarLeituraUseCase : BaseUseCase<CadastrarLeituraDTO>, ICadastrarLeituraUseCase
{
    public CadastrarLeituraUseCase(IUnitOfWork unitOfWork, IValidator<CadastrarLeituraDTO>? validator, ILoggerFactory loggerFactory)
        : base(unitOfWork, validator, loggerFactory) { }

    protected override async Task<Result<object>> ExecuteCoreAsync(CadastrarLeituraDTO dto, CancellationToken cancellation = default)
    {
        var talhao = await _unitOfWork.TalhaoRepository.ObterAsync(dto.TalhaoId);

        if (talhao is null)
            return Result.NotFound("Talhão não localizado.");
        
        var tipo = TipoSensorParser.Parse(dto.Tipo);
        var leituraAgregada = await _unitOfWork.LeituraAgregadaRepository.ObterDoHorario(talhao.Id, tipo, dto.Timestamp);
    }
}
