using System.Diagnostics;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using AzureMongoDbOnion03.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AzureMongoDbOnion03.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var aaa = await _repository.GetAllDebtors();

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
