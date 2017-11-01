using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureMongoDbOnion03.Infrastructure.Data;
using MongoDB.Driver;
using Dto = AzureMongoDbOnion03.Infrastructure.Dto;

namespace AzureMongoDbOnion03.Domain.Services.DbServices
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

        public async Task<IEnumerable<Credit>> GetAllCredits()
        {
            var dtoCredits = await _creditRepository.GetAll();
            return _mapper.Map<IEnumerable<Dto.Credit>, IEnumerable<Credit>>(dtoCredits);
        }

        public Task AddDebtor(Debtor debtor)
        {
            throw new System.NotImplementedException();
        }

        public Task AddCredit(Credit credit)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeleteResult> DeleteDebtor(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeleteResult> DeleteCredit(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
