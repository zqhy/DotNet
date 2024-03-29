using System.Text.Json;
using Hy.Helper;
using Hy.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hy;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHyService(this IServiceCollection services, string? environment, string? instanceId)
    {
        if (!string.IsNullOrEmpty(environment))
        {
            HostingEnvironmentHelper.SetEnvironment(environment);
        }
        
        if (!string.IsNullOrEmpty(instanceId))
        {
            ProjectHelper.SetInstanceId(instanceId);
        }
        
        services.AddSingleton<IJsonSerializer, JsonSerializerImpl>();

        return services;
    }
    
    public static IServiceProvider UseHyService(this IServiceProvider serviceProvider, JsonSerializerOptions? jsonSerializerOptions = null)
    {
        jsonSerializerOptions ??= serviceProvider.GetService<IOptions<JsonSerializerOptions>>()?.Value;
        if (jsonSerializerOptions != null)
        {
            JsonConfig.Set(jsonSerializerOptions);
        }
        
        return serviceProvider;
    }
}