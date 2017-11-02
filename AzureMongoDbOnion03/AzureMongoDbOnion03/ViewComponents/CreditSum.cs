using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain.Services.DbServices;
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

        public async Task<string> Invoke()
        {
            var result = string.Empty;
            var credits = await _dbService.GetAllCredits();

            if (credits != null)
            {
                result = $"Вам должны: \n USD - {credits.Where(x => x.Currency == "USD").Sum(s => s.Amount)} \n" +
                         $"EUR - {credits.Where(x => x.Currency == "EUR").Sum(s => s.Amount)} \n" +
                         $"BYN - {credits.Where(x => x.Currency == "BYN").Sum(s => s.Amount)} \n";
            }

            return result;
        }
    }
}
