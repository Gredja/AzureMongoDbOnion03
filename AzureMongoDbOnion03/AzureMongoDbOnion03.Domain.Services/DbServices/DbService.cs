using System.Collections.Generic;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Infrastructure.Data;

namespace AzureMongoDbOnion03.Domain.Services.DbServices
{
   public class DbService : IDbService
    {
        private readonly IRepository _repository;

        public DbService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Debtor>> GetAllDebtors()
        {
           var aaa = await _repository.GetAllDebtors();
            return null;
        }
    }
}
