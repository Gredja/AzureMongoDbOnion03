using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AzureMongoDbOnion03.Infrastructure.Data;
using MongoDB.Driver;
using Dto = AzureMongoDbOnion03.Infrastructure.Dto.Model;

namespace AzureMongoDbOnion03.Domain.Services.Services.DbServices
{
    public class DbService : IDbService
    {
        private readonly IRepository<Dto.Debtor> _debtorRepository;
        private readonly IRepository<Dto.Credit> _creditRepository;
        private readonly IRepository<Dto.User> _userRepository;
        private readonly IMapper _mapper;

        public DbService(IRepository<Dto.Debtor> debtorRepository, IRepository<Dto.Credit> creditRepository, IRepository<Dto.User> userRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _creditRepository = creditRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Debtor>> GetAllDebtors()
        {
            var dtoDebtors = await _debtorRepository.GetAll();
            return _mapper.Map<IEnumerable<Dto.Debtor>, IEnumerable<Debtor>>(dtoDebtors);
        }

        public async Task<Debtor> GetDebtorById(string id)
        {
            var debtor = await _debtorRepository.GetOne(id);
            return _mapper.Map<Dto.Debtor, Debtor>(debtor);
        }

        public async Task<IEnumerable<Credit>> GetAllCredits(bool active = true)
        {
            var dtoCredits = await _creditRepository.GetAll();
            var dtoActiveCredits = dtoCredits.Where(x => x.Active == active);

            return _mapper.Map<IEnumerable<Dto.Credit>, IEnumerable<Credit>>(dtoActiveCredits);
        }

        public async Task<IEnumerable<Credit>> GetAllCreditsByDebtorId(string userId)
        {
            var dtoCredits = await _creditRepository.GetAll();
            var dtoCreditsByDebtorName = dtoCredits.Where(x => x.ForeignId == userId).ToList();

            return _mapper.Map<IEnumerable<Dto.Credit>, IEnumerable<Credit>>(dtoCreditsByDebtorName);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var dtoUsers = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<Dto.User>, IEnumerable<User>>(dtoUsers);
        }

        public async Task AddDebtor(Debtor debtor)
        {
            var dtoDebtor = _mapper.Map<Debtor, Dto.Debtor>(debtor);
            await _debtorRepository.AddOne(dtoDebtor);
        }

        public async Task AddCredit(Credit credit)
        {
            var dtoCredit = _mapper.Map<Credit, Dto.Credit>(credit);
            await _creditRepository.AddOne(dtoCredit);
        }

        public async Task<DeleteResult> DeleteDebtor(Debtor debtor)
        {
            DeleteResult debtorDeleteResult = null;

            var credirDeleteResult = await _creditRepository.DeleteMany(debtor.Id);

            if (credirDeleteResult.IsAcknowledged)
            {
                debtorDeleteResult = await _debtorRepository.DeleteOne(debtor.Id);
            }

            return debtorDeleteResult;
        }

        public async Task<DeleteResult> DeleteCredit(Credit credit)
        {
            return await _creditRepository.DeleteOne(credit.Id);
        }

        public async Task<ReplaceOneResult> UpdateDebtor(Debtor debtor)
        {
            var dtoDebtor = _mapper.Map<Debtor, Dto.Debtor>(debtor);
            return await _debtorRepository.Update(dtoDebtor);
        }

        public async Task<ReplaceOneResult> UpdateCredit(Credit credit)
        {
            return await Update(credit);
        }

        public async Task<ReplaceOneResult> RepayCredit(Credit credit)
        {
            return await Update(credit);
        }

        private async Task<ReplaceOneResult> Update(Credit credit)
        {
            var dtoCredit = _mapper.Map<Credit, Dto.Credit>(credit);
            return await _creditRepository.Update(dtoCredit);
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
    }
}
