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
        Task<DeleteResult> DeleteDebtor(int id);
        Task<DeleteResult> DeleteCredit(int id);
    }
}
