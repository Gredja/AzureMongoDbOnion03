using System.Collections.Generic;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Infrastructure.Data.Helpers;
using AzureMongoDbOnion03.Infrastructure.Data.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T :class
    {
        private readonly MongoDbContex<T> _context;

        public Repository(IOptions<Settings> settings)
        {
            _context = new MongoDbContex<T>(settings);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Collection.Find(_ => true).ToListAsync();
        }

        public async Task AddOne(T t)
        {
            await _context.Collection.InsertOneAsync(t);
        }

        public async Task<DeleteResult> DeleteOne(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _context.Collection.DeleteOneAsync(filter);
        }

        public async Task<DeleteResult> DeleteMany(int foreignId)
        {
            var filter = Builders<T>.Filter.Eq("ForeignId", foreignId);
            return await _context.Collection.DeleteManyAsync(filter);
        }


        //public async Task<DeleteResult> DeleteDebtor(Dto.Debtor debtor)
        //{
        //    await DeleteCreditsByDebtor(debtor);

        //    var filter = Builders<Dto.Debtor>.Filter.Eq("Id", debtor.Id);
        //    return await _context.Debtors.DeleteOneAsync(filter);
        //}

        //public async Task<UpdateResult> UpdateDebtor(Dto.Debtor debtor)
        //{
        //    var filter = Builders<Dto.Debtor>.Filter.Eq(s => s.Id, debtor.Id);
        //    var update = Builders<Dto.Debtor>.Update.Set(s => s, debtor);

        //    return await _context.Debtors.UpdateOneAsync(filter, update);
        //}

        //public async Task<IEnumerable<Dto.Credit>> GetAllCredits(bool active)
        //{
        //    var filter = Builders<Dto.Credit>.Filter.Eq("Active", active);
        //    return await _context.Credits.Find(filter).ToListAsync();
        //}

        //public async Task<IEnumerable<Dto.Credit>> GetCreditsByDebtor(Dto.Debtor debtor)
        //{
        //    var filter = Builders<Dto.Credit>.Filter.Eq(s => s.DebtorId, debtor.Id);
        //    return await _context.Credits.Find(filter).ToListAsync();
        //}

        //public async Task<DeleteResult> DeleteCredit(Dto.Credit credit)
        //{
        //    var filter = Builders<Dto.Credit>.Filter.Eq("Id", credit.Id);
        //    return await _context.Credits.DeleteOneAsync(filter);
        //}

        //public async Task AddCredit(Dto.Credit credit)
        //{
        //    await _context.Credits.InsertOneAsync(credit);
        //}

        //public async Task<UpdateResult> UpdateCredit(Dto.Credit credit)
        //{
        //    var filter = Builders<Dto.Credit>.Filter.Eq(s => s.Id, credit.Id);
        //    var update = Builders<Dto.Credit>.Update.Set(s => s, credit);

        //    return await _context.Credits.UpdateOneAsync(filter, update);
        //}

        //public async Task<UpdateResult> RepayCredit(Dto.Credit credit)
        //{
        //    var filter = Builders<Dto.Credit>.Filter.Eq(s => s.Id, credit.Id);
        //    var update = Builders<Dto.Credit>.Update.Set(s => s.Active, false);

        //    return await _context.Credits.UpdateOneAsync(filter, update);
        //}

        //private async Task<DeleteResult> DeleteCreditsByDebtor(Dto.Debtor debtor)
        //{
        //    var filter = Builders<Dto.Credit>.Filter.Eq("DebtorId", debtor.Id);
        //    return await _context.Credits.DeleteManyAsync(filter);
        //}

 
    }
}
