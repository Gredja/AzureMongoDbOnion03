using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AzureMongoDbOnion03.Controllers
{

    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IDbService _dbService;
        private readonly IAunification _aunification;

        public HomeController(IDbService dbService, IAunification aunification)
        {
            _dbService = dbService;
            _aunification = aunification;
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

        public IActionResult Edit(Credit credit)
        {
            if (credit != null)
            {
                return RedirectToAction("Index", "CreditEdit", credit);
            }

            return RedirectToAction("Index");
        }
    }
}
