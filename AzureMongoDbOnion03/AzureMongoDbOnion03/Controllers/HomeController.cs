using System.Diagnostics;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain.Services.DbServices;
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

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
