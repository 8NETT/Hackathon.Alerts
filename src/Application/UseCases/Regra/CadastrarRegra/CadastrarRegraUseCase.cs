using Application.DTOs;
using Application.Mapping;
using Application.Persistence;

namespace Application.UseCases.Regra.CadastrarRegra;

public sealed class CadastrarRegraUseCase : BaseUseCase<CadastrarRegraDTO, RegraDTO>, ICadastrarRegraUseCase
{
    public CadastrarRegraUseCase(IUnitOfWork unitOfWork, IValidator<CadastrarRegraDTO>? validator, ILoggerFactory loggerFactory)
        : base(unitOfWork, validator, loggerFactory) { }

    protected override async Task<Result<RegraDTO>> ExecuteCoreAsync(CadastrarRegraDTO dto, CancellationToken cancellation = default)
    {
        var regra = dto.ToEntity();

        _unitOfWork.RegraDeAlertaRepository.Cadastrar(regra);
        await _unitOfWork.SalvarAsync(cancellation);

        return regra.ToDTO();
    }
}
