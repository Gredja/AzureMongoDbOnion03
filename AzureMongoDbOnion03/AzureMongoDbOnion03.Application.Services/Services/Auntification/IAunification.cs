using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Model;

namespace AzureMongoDbOnion03.Application.Services.Services.Auntification
{
    public interface IAunification
    {
        Task<string> TryLogin(User user);
    }
}
