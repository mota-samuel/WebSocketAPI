using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebSocket_Domain.ServicesConnection;
using WebSocket_Infrastructure.OBSConnect;
using WebSocket_Infrastructure.OBSServices;

namespace WebSocket_Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddObsService(this IServiceCollection services, IConfiguration config)
    {
       AddServices(services, config);
    }

    private static void AddServices(IServiceCollection services, IConfiguration config)
    {
        services.Configure<OBSSettings>(config.GetSection("Settings:OBS"));

        services.AddSingleton<IObsService, OBSService>();
    }
}
