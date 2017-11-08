using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;

namespace AzureMongoDbOnion03.Application.Services.Auntification
{
    public class Aunification : IAunification
    {
        private readonly IDbService _dbService;

        public Aunification(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<User> TryLogin(User user)
        {
            var users = await _dbService.GetAllUsers();
            return Enumerable.Where<User>(users, x => x.Email == user.Email).FirstOrDefault(x => x.Password == user.Password);
        }

        public Task LogOut()
        {
            //TODO
            throw new System.NotImplementedException();
        }
    }
}
