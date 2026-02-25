using Application.Mapping;
using Application.Persistence;

namespace Application.UseCases.Talhao.CadastrarTalhao;

public sealed class CadastrarTalhaoUseCase : BaseUseCase<CadastrarTalhaoDTO>, ICadastrarTalhaoUseCase
{
    public CadastrarTalhaoUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected override async Task<Result<object>> ExecuteCoreAsync(CadastrarTalhaoDTO dto, CancellationToken cancellation = default)
    {
        var talhao = await _unitOfWork.TalhaoRepository.ObterAsync(dto.Id, cancellation);

        if (talhao is not null)
            return Result.Conflict($"Talhão com ID {talhao.Id} já se cadastrado.");

        _unitOfWork.TalhaoRepository.Cadastrar(dto.ToEntity());
        await _unitOfWork.SalvarAsync(cancellation);

        return Result.Success();
    }
}
