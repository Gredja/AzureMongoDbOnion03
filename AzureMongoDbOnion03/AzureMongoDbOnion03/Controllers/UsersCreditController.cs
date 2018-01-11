using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Diagnostics.Contracts.Contract;

namespace AzureMongoDbOnion03.Controllers
{
    [Authorize]
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

            var sumAmount = CreateAmmoutSum(null);
            var userCreditsViewModel  = new UserCreditsViewModel
            {
                Credits = sumAmount
            };
          
            return View(userCreditsViewModel);
        }

        private IEnumerable<Credit> CreateAmmoutSum(IEnumerable<Credit> credits)
        {
            var sumAmount = from credit in credits
                group credit by credit.Currency
                into res
                select new Credit
                {
                    Amount = res.Sum(x => x.Amount),
                    Currency = res.Select(x => x.Currency).FirstOrDefault()
                };

            return sumAmount;
        }

        [Pure]
        [ContractInvariantMethod]
        private void ContractInvariant()
        {
            Invariant(this._dbService != null);
        }
    }
}
