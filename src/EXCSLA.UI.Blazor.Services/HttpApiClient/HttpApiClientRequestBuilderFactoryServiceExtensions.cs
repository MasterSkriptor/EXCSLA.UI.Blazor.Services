using Microsoft.Extensions.DependencyInjection;

namespace EXCSLA.UI.Blazor.Services
{
    public static class HttpApiClientRequestBuilderFactoryServiceExtensions
    {
        public static void AddApiClientRequestBuilder(this IServiceCollection services)
        {
            services.AddScoped<IHttpApiClientRequestBuilderFactory, HttpApiClientRequestBuilderFactory>();
        }
    }
}
