using Clean_architecture.Application.Common.Repository;
using Clean_architecture.Domain.Entities;
using Clean_architecture.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Clean_architecture.Infrastructure.Services;
public static class ClientConnectionService
{
    public static IServiceCollection PrepareClientConnection(this IServiceCollection service)
    {
        #region Build service provider
        var _serviceProvider = service.BuildServiceProvider();
        var scope = _serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
        // created logger service
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var _logger = loggerFactory.CreateLogger("app");
        #endregion

        var clientBuilder = services.GetRequiredService<ClientBuilder>();
        var connectionstring = clientBuilder.BuildConnectionSettings<demoindex>();
        var client = clientBuilder.CreateIndexIfNotExists<demoindex>(nameof(demoindex), connectionstring);
        _logger.Log(LogLevel.Information, "client created successfully");
        service.AddScoped(s => new IndexRepository<demoindex>(client));
        _logger.Log(LogLevel.Information, "Repo is woking fine now");

        return service;
    }
}
