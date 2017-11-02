using System.Diagnostics;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.AspNetCore.Mvc;
using AzureMongoDbOnion03.Models;

namespace AzureMongoDbOnion03.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbService _dbService;

        public HomeController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index()
        {
            //var aaa = await _dbService.GetAllDebtors();

            return View();
        }

        public async Task<IActionResult> Repay(Credit credit)
        {
            //TODO
           // await _dbService.RepayCredit(credit);

            return RedirectToAction("Index");
        }
    }
}
