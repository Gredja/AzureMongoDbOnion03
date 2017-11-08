using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Model;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Application.Services.Services.Auntification
{
    public interface IAunification
    {
        Task AddUser(User user);
        Task<DeleteResult> DeleteUser(User user);
        Task<ReplaceOneResult> UpdateUser(User user);
        Task<User> TryLogin(User user);
    }
}
