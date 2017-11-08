using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureMongoDbOnion03.Application.Services.Model;
using AzureMongoDbOnion03.Infrastructure.Data;
using MongoDB.Driver;
using Dto = AzureMongoDbOnion03.Infrastructure.Dto.Model;

namespace AzureMongoDbOnion03.Application.Services.Services.Auntification
{
    public class Aunification : IAunification
    {
        private readonly IRepository<Dto.User> _userRepository;
        private readonly IMapper _mapper;

        public Aunification(IRepository<Dto.User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task AddUser(User user)
        {
            var dtoUser = _mapper.Map<User, Dto.User>(user);
            await _userRepository.AddOne(dtoUser);
        }

        public async Task<DeleteResult> DeleteUser(User user)
        {
            return await _userRepository.DeleteOne(user.Id);
        }

        public async Task<ReplaceOneResult> UpdateUser(User user)
        {
            var dtoUser = _mapper.Map<User, Dto.User>(user);
            return await _userRepository.Update(dtoUser);
        }

        public async Task<User> TryLogin(User user)
        {
            var users = await _userRepository.GetAll();
            var regUser = users.Where(x => x.Email == user.Email).FirstOrDefault(x => x.Password == user.Password);
            return _mapper.Map<Dto.User, User>(regUser);
        }
    }
}
