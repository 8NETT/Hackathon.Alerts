using Application.Persistence;
using Domain.Messaging;
using Domain.Persistence;
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
        services.AddScoped<ILeituraAgregadaPersistence, LeituraAgregadaRepository>();

        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddTalhaoEventHub(configuration)
            .AddLeituraEventHub(configuration)
            .AddAlertaEventHub(configuration);

    private static IServiceCollection AddTalhaoEventHub(this IServiceCollection services, IConfiguration configuration) =>
        services.AddHostedService(sp =>
        {
            var connectionString = configuration["TalhaoEventHub:ConnectionString"];
            var consumerGroup = configuration["TalhaoEventHub:ConsumerGroup"];

            if (string.IsNullOrWhiteSpace(connectionString) ||
                string.IsNullOrWhiteSpace(consumerGroup))
                throw new InvalidOperationException("Configuração do hub de talhão não localizada no arquivo de configuração.");

            return new TalhaoEventsConsumerHostedService(
                sp.GetRequiredService<IServiceScopeFactory>(),
                consumerGroup,
                connectionString,
                sp.GetService<ILogger<TalhaoEventsConsumerHostedService>>());
        });

    private static IServiceCollection AddLeituraEventHub(this IServiceCollection services, IConfiguration configuration) =>
        services.AddHostedService(sp =>
        {
            var connectionString = configuration["LeituraEventHub:ConnectionString"];
            var consumerGroup = configuration["LeituraEventHub:ConsumerGroup"];

            if (string.IsNullOrWhiteSpace(connectionString) ||
                string.IsNullOrWhiteSpace(consumerGroup))
                throw new InvalidOperationException("Configuração do hub de talhão não localizada no arquivo de configuração.");

            return new LeituraEventsConsumerHostedService(
                sp.GetRequiredService<IServiceScopeFactory>(),
                consumerGroup,
                connectionString,
                sp.GetService<ILogger<LeituraEventsConsumerHostedService>>());
        });

    private static IServiceCollection AddAlertaEventHub(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["AlertaEventHub:ConnectionString"]
            ?? throw new InvalidOperationException("Configuração do hub de talhão não localizada no arquivo de configuração.");

        services.AddSingleton(new EventHubProducerClient(connectionString));
        services.AddScoped<IAlertaPublisher, AlertaEventPublisher>();
        services.AddScoped<IAntiSpamDeAlertas, AntiSpamDeAlertasEmMemoria>();

        return services;
    }
}
