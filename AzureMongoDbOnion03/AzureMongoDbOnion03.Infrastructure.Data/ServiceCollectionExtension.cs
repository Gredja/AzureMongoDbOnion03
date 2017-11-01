using Microsoft.Extensions.DependencyInjection;

namespace AzureMongoDbOnion03.Infrastructure.Data
{
   public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
