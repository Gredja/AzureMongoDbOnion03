using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Models;

namespace AzureMongoDbOnion03
{
    public interface IAunification
    {
        Task<User> TryLogin(AunificatedUser user);
        Task LogOut();
    }
}
