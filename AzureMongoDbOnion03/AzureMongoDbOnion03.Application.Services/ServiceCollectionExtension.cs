using AzureMongoDbOnion03.Application.Services.Auntification;
using Microsoft.Extensions.DependencyInjection;

namespace AzureMongoDbOnion03.Application.Services
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
