
using AzureMongoDbOnion03.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AzureMongoDbOnion03.Providers
{
    public static class Providers
    {
        public static void AddDbrepository(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
        }
    }
}
