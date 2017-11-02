using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureMongoDbOnion03.Infrastructure.Data;
using MongoDB.Driver;
using Dto = AzureMongoDbOnion03.Infrastructure.Dto.Model;

namespace AzureMongoDbOnion03.Domain.Services.Services.DbServices
{
    public class DbService : IDbService
    {
        private readonly IRepository<Infrastructure.Dto.Model.Debtor> _debtorRepository;
        private readonly IRepository<Dto.Credit> _creditRepository;
        private readonly IMapper _mapper;

        public DbService(IRepository<Infrastructure.Dto.Model.Debtor> debtorRepository, IRepository<Dto.Credit> creditRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _creditRepository = creditRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Debtor>> GetAllDebtors()
        {
            var dtoDebtors = await _debtorRepository.GetAll();
            return _mapper.Map<IEnumerable<Infrastructure.Dto.Model.Debtor>, IEnumerable<Debtor>>(dtoDebtors);
        }

        public async Task<IEnumerable<Credit>> GetAllCredits()
        {
            var dtoCredits = await _creditRepository.GetAll();
            return _mapper.Map<IEnumerable<Dto.Credit>, IEnumerable<Credit>>(dtoCredits);
        }

        public async Task AddDebtor(Debtor debtor)
        {
            var dtoDebtor = _mapper.Map<Debtor, Infrastructure.Dto.Model.Debtor>(debtor);
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

        public Task<UpdateResult> UpdateDebtor(Debtor debtor)
        {
            //TODO
            return null;
        }
    }
}
