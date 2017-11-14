using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Controllers
{
    public class UsersCreditController : Controller
    {
        private readonly IDbService _dbService;

        public UsersCreditController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var debtor = await _dbService.GetDebtorById(userId);
            if (debtor != null)
            {
                ViewData["DebtorName"] = debtor.Name;
            }

            var credits = await _dbService.GetAllCreditsByDebtorId(userId);
            var sumAmount = from credit in credits
                group credit by credit.Currency
                into res
                select new Credit
                {
                    Amount = res.Sum(x => x.Amount),
                    Currency = res.Select(x => x.Currency).FirstOrDefault()
                };

            return View(sumAmount);
        }
    }
}
