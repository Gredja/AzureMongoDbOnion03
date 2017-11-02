using System;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Controllers
{
    public class DebtorController : Controller
    {
        private readonly IDbService _dbService;

        public DebtorController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index()
        {
            var debtors = await _dbService.GetAllDebtors();

            return View(debtors);
        }

        [HttpPost]
        public async Task<IActionResult> AddDebtor(Debtor debtor)
        {
            if (ModelState.IsValid)
            {
                await _dbService.AddDebtor(new Debtor
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = debtor.Name
                });
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteDebtor(Debtor debtor)
        {
            await _dbService.DeleteDebtor(debtor);

            return RedirectToAction("Index");
        }
    }
}
