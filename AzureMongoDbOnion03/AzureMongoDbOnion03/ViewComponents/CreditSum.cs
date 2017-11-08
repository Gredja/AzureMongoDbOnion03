using System.Linq;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.ViewComponents
{
    public class CreditSum : ViewComponent
    {
        private readonly IDbService _dbService;

        public CreditSum(IDbService dbService)
        {
            _dbService = dbService;
        }

        public string Invoke()
        {
            var result = string.Empty;
            var credits =  _dbService.GetAllCredits(active: true).Result;

            if (credits != null)
            {
                var creditsArray = credits as Credit[] ?? credits.ToArray();

                result = $"Вам должны: \n USD - {creditsArray.Where(x => x.Currency == "USD").Sum(s => s.Amount)} \n" +
                         $"EUR - {creditsArray.Where(x => x.Currency == "EUR").Sum(s => s.Amount)} \n" +
                         $"BYN - {creditsArray.Where(x => x.Currency == "BYN").Sum(s => s.Amount)} \n";
            }

            return result;
        }
    }
}
