
using Application.Persistence;

namespace Application.UseCases.Talhao.RemoverTalhao;

public sealed class RemoverTalhaoUsecase : BaseUseCase<Guid>, IRemoverTalhaoUseCase
{
    public RemoverTalhaoUsecase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected override async Task<Result<object>> ExecuteCoreAsync(Guid id, CancellationToken cancellation = default)
    {
        var talhao = await _unitOfWork.TalhaoRepository.ObterAsync(id, cancellation);

        if (talhao is null)
            return Result.NotFound($"Talhão com ID {id} não localizado.");

        _unitOfWork.TalhaoRepository.Remover(talhao);
        await _unitOfWork.SalvarAsync(cancellation);

        return Result.Success();
    }
}
