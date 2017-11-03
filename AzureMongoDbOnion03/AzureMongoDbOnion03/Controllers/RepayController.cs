using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
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

        public IActionResult Index()
        {
            return View();
        }
    }
}
