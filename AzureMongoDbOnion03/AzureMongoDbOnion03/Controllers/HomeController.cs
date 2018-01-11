using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.Helpers;
using AzureMongoDbOnion03.Infrastructure.Dto.Model;
using AzureMongoDbOnion03.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Credit = AzureMongoDbOnion03.Domain.Credit;
using Debtor = AzureMongoDbOnion03.Domain.Debtor;
using User = AzureMongoDbOnion03.Domain.User;

namespace AzureMongoDbOnion03.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IDbService _dbService;
        private readonly ILogger _logger;

        public HomeController(IDbService dbService, ILogger<HomeController> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string currency = "")
        {
            await CreateFillDataBase();

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

            _logger.LogInformation("!!!!Start", "Index");

            return View(viewModel);
        }

        private async Task CreateFillDataBase()
        {
            if (!_dbService.RoleCollectionExistence())
            {
                //await _dbService.AddRole(new Role { Id = (int)Roles.Admin, Name = Roles.Admin.ToString() });
                //await _dbService.AddRole(new Role { Id = (int)Roles.User, Name = Roles.User.ToString() });
            }

            if (!_dbService.UserCollectionExistence())
            {
                var roles = await _dbService.GetAllRoles();

                if (roles != null)
                {
                    await _dbService.AddUser(new User
                    {
                        Email = "gredja.a@gmail.com",
                        ForeignId = "",
                        Id = Guid.NewGuid().ToString(),
                        Password = "12345",
                        RoleId = int.Parse(roles.FirstOrDefault(x =>x.Name == Roles.Admin.ToString()).Id)
                    });
                }
            }
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
