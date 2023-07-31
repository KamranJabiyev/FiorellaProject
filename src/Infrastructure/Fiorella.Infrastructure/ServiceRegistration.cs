
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Aplication.Abstraction.Storage;
using Fiorella.Infrastructure.Services.Storage;
using Fiorella.Infrastructure.Services.Storage.Local;
using Fiorella.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Fiorella.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IStorageService,StorageService>();
    }

    public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
    {
        services.AddScoped<IStorage, T>();
    }

}
