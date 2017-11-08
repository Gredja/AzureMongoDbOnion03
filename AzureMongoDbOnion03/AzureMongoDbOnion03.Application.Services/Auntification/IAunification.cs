using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;

namespace AzureMongoDbOnion03.Application.Services.Auntification
{
    public interface IAunification
    {
        Task<User> TryLogin(User user);
        Task LogOut();
    }
}
