
using Fiorella.Aplication.Abstraction.Services;
using Fiorella.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Fiorella.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
    }
}
