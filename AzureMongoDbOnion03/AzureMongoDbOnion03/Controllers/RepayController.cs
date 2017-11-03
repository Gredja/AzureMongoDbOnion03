using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Controllers
{
    public class RepayController : Controller
    {
        private readonly IDbService _dbService;

        public RepayController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index()
        {
            var debtors = await _dbService.GetAllDebtors();
            var credits = await _dbService.GetAllCredits(active: false);

            var creditsArray = credits as Credit[] ?? credits.ToArray();


            var debtorsArray = debtors as Debtor[] ?? debtors.ToArray();

            var moneyPlusDebtorName = creditsArray.Join(debtorsArray, arg => arg.ForeignId,
                arg => arg.Id, (credit, debtor) => new MoneyPlusDebtorName {Credit = credit, DebtorName = debtor.Name});

            return View(moneyPlusDebtorName);
        }

        public async Task<IActionResult> Delete(Credit credit)
        {
            await _dbService.DeleteCredit(credit);
            return RedirectToAction("Index");
        }
    }
}
