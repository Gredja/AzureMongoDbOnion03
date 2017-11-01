using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureMongoDbOnion03.Domain.Services.DbServices
{
   public interface IDbService
    {
        Task<IEnumerable<Debtor>> GetAllDebtors();
    }
}
