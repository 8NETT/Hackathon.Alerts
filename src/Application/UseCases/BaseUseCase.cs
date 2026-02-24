using Application.Persistence;

namespace Application.UseCases;

public abstract class BaseUseCase<TInput> : BaseUseCase<TInput, object>, IUseCase<TInput>
{
    protected internal BaseUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        : base(unitOfWork, loggerFactory) { }

    protected internal BaseUseCase(IUnitOfWork unitOfWork, IValidator<TInput>? validator, ILoggerFactory loggerFactory)
        : base(unitOfWork, validator, loggerFactory) { }

    public new async Task<Result> HandleAsync(TInput input, CancellationToken cancellation = default)
    {
        var result = await base.HandleAsync(input, cancellation);
        return result.Map();
    }
}

public abstract class BaseUseCase<TInput, TOutput> : IUseCase<TInput, TOutput>
{
    protected IUnitOfWork _unitOfWork;
    protected IValidator<TInput>? _validator;
    protected ILogger _logger;
    protected string _useCaseName;

    protected internal BaseUseCase(IUnitOfWork unitOfWork, ILoggerFactory loggerFactory) : this(unitOfWork, null, loggerFactory) { }

    protected internal BaseUseCase(IUnitOfWork unitOfWork, IValidator<TInput>? validator, ILoggerFactory loggerFactory)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _useCaseName = GetType().Name;
        _logger = loggerFactory.CreateLogger(_useCaseName);
    }

    public async Task<Result<TOutput>> HandleAsync(TInput input, CancellationToken cancellation = default)
    {
        using var scope = _logger.BeginScope(new Dictionary<string, object?> { ["UseCase"] = _useCaseName });

        // Validation
        var validation = _validator?.Validate(input);
        if (validation is not null && !validation.IsValid)
        {
            _logger.LogWarning("Validação falhou em {UseCase} com {ErrorCount} erro(s): {@Errors}", _useCaseName, validation.Errors.Count, validation.Errors);
            return Result.Invalid(validation.AsErrors());
        }

        // Execution
        var sw = Stopwatch.StartNew();
        var result = await ExecuteCoreAsync(input, cancellation);
        sw.Stop();

        // Result
        if (result.IsSuccess)
            _logger.LogInformation("{UseCase} concluído com sucesso em {ElapsedMs} ms", _useCaseName, sw.Elapsed);
        else
            _logger.LogWarning("{UseCase} concluído com falha em {ElapsedMs} ms: {@Result}", _useCaseName, sw.Elapsed, result);

        return result;
    }

    protected abstract Task<Result<TOutput>> ExecuteCoreAsync(TInput input, CancellationToken cancellation = default);
}
