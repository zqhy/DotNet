using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hy
{
    public static class ServiceProviderExtensions
    {
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
}