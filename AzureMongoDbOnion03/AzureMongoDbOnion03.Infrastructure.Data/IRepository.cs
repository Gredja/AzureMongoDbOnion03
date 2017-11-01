﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace AzureMongoDbOnion03.Infrastructure.Data
{
    public interface IRepository
    {
        Task<IEnumerable<Dto.Debtor>> GetAllDebtors();
        Task AddOneDebtor(Dto.Debtor debtor);
        Task<DeleteResult> DeleteDebtor(Dto.Debtor debtor);
        Task<UpdateResult> UpdateDebtor(Dto.Debtor debtor);
        Task<IEnumerable<Dto.Credit>> GetAllCredits(bool active);
        Task<IEnumerable<Dto.Credit>> GetCreditsByDebtor(Dto.Debtor debtor);
        Task<DeleteResult> DeleteCredit(Dto.Credit credit);
        Task AddCredit(Dto.Credit credit);
        Task<UpdateResult> UpdateCredit(Dto.Credit credit);
        Task<UpdateResult> RepayCredit(Dto.Credit credit);
    }
}
