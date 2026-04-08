using Microsoft.Extensions.DependencyInjection;

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
    }
}
