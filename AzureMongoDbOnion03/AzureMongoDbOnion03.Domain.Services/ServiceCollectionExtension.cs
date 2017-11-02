using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.Extensions.DependencyInjection;

namespace AzureMongoDbOnion03.Domain.Services
{
   public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbService(this IServiceCollection services)
        {
            services.AddTransient<IDbService, DbService>();
            return services;
        }

    }
}
