using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Domain.Services.DbServices
{
    public interface IDbService
    {
        Task<IEnumerable<Debtor>> GetAllDebtors();
        Task<IEnumerable<Credit>> GetAllCredits();
        Task AddDebtor(Debtor debtor);
        Task AddCredit(Credit credit);
        Task<DeleteResult> DeleteDebtor(string id);
        Task<DeleteResult> DeleteCredit(string id);
    }
}
