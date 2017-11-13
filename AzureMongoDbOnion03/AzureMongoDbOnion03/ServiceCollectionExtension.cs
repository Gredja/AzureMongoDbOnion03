using Microsoft.Extensions.DependencyInjection;

namespace AzureMongoDbOnion03
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
