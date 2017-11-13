using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Models;
using Microsoft.AspNetCore.Http;

namespace AzureMongoDbOnion03
{
    public interface IAunification
    {
        Task<User> TryLogin(AunificatedUser user);
        Task LogOut();
    }
}
