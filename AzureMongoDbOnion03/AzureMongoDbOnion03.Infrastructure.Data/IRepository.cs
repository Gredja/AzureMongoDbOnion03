using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Infrastructure.Data
{
    public interface IRepository<T> where T : class
    {
        bool CheckExistence();
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(string id);
        Task AddOne(T t);
        Task AddMany(IEnumerable<T> t); 
        Task<DeleteResult> DeleteOne(string id);
        Task<DeleteResult> DeleteMany(string foreignId);
        Task<ReplaceOneResult> Update(T t);
    }
}
