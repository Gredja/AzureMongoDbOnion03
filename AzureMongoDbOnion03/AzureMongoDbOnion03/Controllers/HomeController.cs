using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AzureMongoDbOnion03.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbService _dbService;

        public HomeController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index(string currency = "")
        {
            var debtors = await _dbService.GetAllDebtors();
            var credits = await _dbService.GetAllCredits();

            IEnumerable<MoneyPlusDebtorName> creditPlusDebtorNames;

            var creditsArray = credits as Credit[] ?? credits.ToArray();
            var debtorsArray = debtors as Debtor[] ?? debtors.ToArray();

            if (!string.IsNullOrEmpty(currency))
            {
                creditPlusDebtorNames = creditsArray.Join(debtorsArray, arg => arg.ForeignId, arg => arg.Id,
                        (credit, debtor) => new MoneyPlusDebtorName { Credit = credit, DebtorName = debtor.Name })
                    .Where(arg => arg.Credit.Currency == currency);
            }
            else
            {
                creditPlusDebtorNames = creditsArray.Join(debtorsArray, arg => arg.ForeignId, arg => arg.Id,
                    (credit, debtor) => new MoneyPlusDebtorName { Credit = credit, DebtorName = debtor.Name });
            }

            var viewModel = new HomeIndexViewModel
            {
                Credits = creditsArray,
                Debtors = debtorsArray,
                CreditPlusDebtorNames = creditPlusDebtorNames,
                NewCredit = new Credit()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddCredit(HomeIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.NewCredit.Id = Guid.NewGuid().ToString();
                await _dbService.AddCredit(viewModel.NewCredit);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Repay(Credit credit)
        {
            var active = credit.GetType().GetProperty("Active");
            active.SetValue(credit, false);

            await _dbService.RepayCredit(credit);

            return RedirectToAction("Index");
        }

        public IActionResult GetCurrency(HomeIndexViewModel viewModel)
        {
            return RedirectToAction("Index", new RouteValueDictionary(new { controller = "Home", action = "Index", currency = viewModel.SelectedCurrency }));
        }
    }
}
