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
        private readonly IMapper _mapper;

        public DbService(IRepository<Dto.Debtor> debtorRepository, IRepository<Dto.Credit> creditRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _creditRepository = creditRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Debtor>> GetAllDebtors()
        {
            var dtoDebtors = await _debtorRepository.GetAll();
            return _mapper.Map<IEnumerable<Dto.Debtor>, IEnumerable<Debtor>>(dtoDebtors);
        }

        public async Task<IEnumerable<Credit>> GetAllCredits(bool active = true)
        {
            var dtoCredits = await _creditRepository.GetAll();
            var dtoActiveCredits = dtoCredits.Where(x => x.Active == active);

            return _mapper.Map<IEnumerable<Dto.Credit>, IEnumerable<Credit>>(dtoActiveCredits);
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
    }
}
