using AzureMongoDbOnion03.Application.Services.Aunification;
using Microsoft.Extensions.DependencyInjection;

namespace AzureMongoDbOnion03.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUserAunification(this IServiceCollection services)
        {
            services.AddTransient<IAunification, Aunification>();
            return services;
        }
    }
}
