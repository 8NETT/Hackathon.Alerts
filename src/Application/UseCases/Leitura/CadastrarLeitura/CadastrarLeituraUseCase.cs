using Application.Mapping;
using Application.Persistence;
using Domain.Entities;
using Domain.Services;
using Domain.ValueObjects;

namespace Application.UseCases.Leitura.CadastrarLeitura;

public sealed class CadastrarLeituraUseCase : BaseUseCase<CadastrarLeituraDTO>, ICadastrarLeituraUseCase
{
    private readonly AvaliadorDeRegras _avaliadorDeRegras;

    public CadastrarLeituraUseCase(
        AvaliadorDeRegras avaliadorDeRegras,
        IUnitOfWork unitOfWork, 
        IValidator<CadastrarLeituraDTO>? validator, 
        ILoggerFactory loggerFactory)
        : base(unitOfWork, validator, loggerFactory)
    {
        _avaliadorDeRegras = avaliadorDeRegras;
    }

    protected override async Task<Result<object>> ExecuteCoreAsync(CadastrarLeituraDTO dto, CancellationToken cancellation = default)
    {
        var talhao = await _unitOfWork.TalhaoRepository.ObterAsync(dto.TalhaoId, cancellation);

        if (talhao is null)
            return Result.NotFound("Talhão não localizado.");
        
        var tipo = TipoSensor.Parse(dto.Tipo);
        var leituraAgregada = await _unitOfWork.LeituraAgregadaRepository.ObterDoHorario(talhao.Id, tipo, dto.Timestamp, cancellation);

        if (leituraAgregada is null)
        {
            leituraAgregada = new LeituraAgregada(dto.ToValueObject());
            _unitOfWork.LeituraAgregadaRepository.Cadastrar(leituraAgregada);
        }
        else
        {
            leituraAgregada.Agregar(dto.ToValueObject());
            _unitOfWork.LeituraAgregadaRepository.Atualizar(leituraAgregada);
        }

        var regras = await _unitOfWork.RegraDeAlertaRepository.ObterRegrasAtivas(tipo, cancellation);

        foreach (var regra in regras)
            await _avaliadorDeRegras.AvaliarAsync(regra, leituraAgregada, cancellation);

        await _unitOfWork.SalvarAsync(cancellation);

        return Result.Success();
    }
}
