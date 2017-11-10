using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Auntification.Model;
using AzureMongoDbOnion03.Domain;
using Microsoft.AspNetCore.Http;

namespace AzureMongoDbOnion03.Application.Services.Auntification
{
    public interface IAunification
    {
        Task<User> TryLogin(AunificatedUser user);
        Task LogOut(HttpContext httpContext);
    }
}
