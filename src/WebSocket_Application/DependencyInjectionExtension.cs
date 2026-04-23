using Microsoft.Extensions.DependencyInjection;
using WebSocket_Application.Services;

namespace WebSocket_Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
        {
            AddServices(services);
        }
    
        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<UseCases.RecordingService>();
            services.AddHostedService<StartupService>();
    }
}
