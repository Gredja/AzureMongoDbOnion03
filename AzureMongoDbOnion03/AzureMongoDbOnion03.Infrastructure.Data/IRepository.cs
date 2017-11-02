using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Infrastructure.Data
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task AddOne(T t);
        Task<DeleteResult> DeleteOne(string id);
        Task<DeleteResult> DeleteMany(string foreignId);
        Task<ReplaceOneResult> Update(T t);


        //    Task<IEnumerable<Dto.Credit>> GetAllCredits(bool active);
        //    Task<IEnumerable<Dto.Credit>> GetCreditsByDebtor(Dto.Debtor debtor);
        //    Task<DeleteResult> DeleteCredit(Dto.Credit credit);
        //    Task AddCredit(Dto.Credit credit);
        //    Task<UpdateResult> UpdateCredit(Dto.Credit credit);
        //    Task<UpdateResult> RepayCredit(Dto.Credit credit);
    }
}
