using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Domain.Services.Services.DbServices
{
    public interface IDbService
    {
        Task<IEnumerable<Debtor>> GetAllDebtors();
        Task<IEnumerable<Credit>> GetAllCredits(bool active = true);
        Task AddDebtor(Debtor debtor);
        Task AddCredit(Credit credit);
        Task<DeleteResult> DeleteDebtor(Debtor debtor);
        Task<DeleteResult> DeleteCredit(Credit credit);
        Task<ReplaceOneResult> UpdateDebtor(Debtor debtor);
        Task<ReplaceOneResult> UpdateCredit(Credit credit);
        Task<ReplaceOneResult> RepayCredit(Credit credit);
    }
}
