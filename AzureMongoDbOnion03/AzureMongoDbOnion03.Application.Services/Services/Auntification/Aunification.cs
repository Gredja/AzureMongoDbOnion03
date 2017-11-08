using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Model;
using AzureMongoDbOnion03.Infrastructure.Data;
using Dto = AzureMongoDbOnion03.Infrastructure.Dto.Model;

namespace AzureMongoDbOnion03.Application.Services.Services.Auntification
{
    public class Aunification : IAunification
    {
        private readonly IRepository<Dto.User> _userRepository;

        public Aunification(IRepository<Dto.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> TryLogin(User user)
        {
            var users = await _userRepository.GetAll();
            return users.Where(x => x.Email == user.Email).FirstOrDefault(x => x.Password == user.Password).Name;
        }
    }
}
