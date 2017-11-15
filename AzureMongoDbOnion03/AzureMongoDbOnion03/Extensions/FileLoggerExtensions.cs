using AzureMongoDbOnion03.Log;
using Microsoft.Extensions.Logging;

namespace AzureMongoDbOnion03.Extensions
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
