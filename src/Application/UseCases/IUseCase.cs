namespace Application.UseCases;

public interface IUseCase<in TInput>
{
    Task<Result> HandleAsync(TInput input, CancellationToken cancellation = default);
}

public interface IUseCase<in TInput, TOutput>
{
    Task<Result<TOutput>> HandleAsync(TInput input, CancellationToken cancellation = default);
}
