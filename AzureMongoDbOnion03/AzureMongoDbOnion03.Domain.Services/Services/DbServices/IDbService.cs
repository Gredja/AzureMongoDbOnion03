using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Dto = AzureMongoDbOnion03.Infrastructure.Dto.Model;


namespace AzureMongoDbOnion03.Domain.Services.Services.DbServices
{
    public interface IDbService
    {
        bool RoleCollectionExistence();
        bool UserCollectionExistence();
        Task<IEnumerable<Dto.Role>> GetAllRoles();
        Task<IEnumerable<Debtor>> GetAllDebtors();
        Task<Debtor> GetDebtorById(string id);
        Task<IEnumerable<Credit>> GetAllCredits(bool active = true);
        Task<IEnumerable<Credit>> GetAllCreditsByDebtorId(string userId);
        Task<IEnumerable<User>> GetAllUsers();
        Task AddRoles(IEnumerable<Dto.Role> roles);
        Task AddDebtor(Debtor debtor);
        Task AddCredit(Credit credit);
        Task<DeleteResult> DeleteDebtor(Debtor debtor);
        Task<DeleteResult> DeleteCredit(Credit credit);
        Task<ReplaceOneResult> UpdateDebtor(Debtor debtor);
        Task<ReplaceOneResult> UpdateCredit(Credit credit);
        Task<ReplaceOneResult> RepayCredit(Credit credit);
        Task AddUser(User user);
        Task<DeleteResult> DeleteUser(User user);
        Task<ReplaceOneResult> UpdateUser(User user);
    }
}
