using Application.Persistence;
using Domain.Messaging;
using Infrastructure.Messaging;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddPersistence(configuration)
            .AddMessaging(configuration);

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
            ?? throw new InvalidOperationException("ConnectionString não localizada na configuração.");

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

        services.AddScoped<ITalhaoRepository, TalhaoRepository>();
        services.AddScoped<ILeituraAgregadaRepository, LeituraAgregadaRepository>();
        services.AddScoped<IRegraDeAlertaRepository, RegraDeAlertaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddTalhaoEventHub(configuration)
            .AddAlertaEventHub(configuration);

    private static IServiceCollection AddTalhaoEventHub(this IServiceCollection services, IConfiguration configuration)
    {
        IServiceCollection serviceCollection = services.AddSingleton(sp =>
        {
            var connectionString = configuration["TalhaoEventHub:ConnectionString"];
            var consumerGroup = configuration["TalhaoEventHub:ConsumerGroup"];

            if (string.IsNullOrWhiteSpace(connectionString) ||
                string.IsNullOrWhiteSpace(consumerGroup))
                throw new InvalidOperationException("Configuração do hub de talhão não localizada no arquivo de configuração.");

            return new EventHubConsumerClient(consumerGroup, connectionString);
        });

        services.AddHostedService<TalhaoEventsConsumerHostedService>();

        return services;
    }

    private static IServiceCollection AddAlertaEventHub(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["AlertaEventHub:ConnectionString"]
            ?? throw new InvalidOperationException("Configuração do hub de talhão não localizada no arquivo de configuração.");

        services.AddSingleton(new EventHubProducerClient(connectionString));
        services.AddScoped<IAlertaPublisher, AlertaEventPublisher>();

        return services;
    }
}
