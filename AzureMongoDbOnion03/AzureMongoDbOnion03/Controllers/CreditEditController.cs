using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Controllers
{
    public class CreditEditController : Controller
    {
        private readonly IDbService _dbService;

        public CreditEditController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index(Credit credit)
        {
            var debtors = await _dbService.GetAllDebtors();

            ViewData["Debtor"] = debtors.Where(x => x.Id == credit.ForeignId).Select(x => x.Name);

            return View(credit);
        }

        public async Task<IActionResult> Save(Credit credit)
        {
            await _dbService.UpdateCredit(credit);
            return RedirectToAction("Index", "Home");
        }
    }
}
