using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Models;
using AzureMongoDbOnion03.Domain;

namespace AzureMongoDbOnion03.Application.Services.Aunification
{
    public interface IAunification
    {
        Task<User> TryLogin(AunificatedUser user);
        Task LogOut();
    }
}
