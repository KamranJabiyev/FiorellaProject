using Microsoft.Extensions.Configuration;

namespace Fiorella.Persistence.Helpers;

internal static class Configuration
{
    internal static string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new();
            configurationManager.SetBasePath(Directory.GetCurrentDirectory());
            configurationManager.AddJsonFile("appsettings.json");
            return configurationManager.GetConnectionString("Default");
        }
        
    }
}
