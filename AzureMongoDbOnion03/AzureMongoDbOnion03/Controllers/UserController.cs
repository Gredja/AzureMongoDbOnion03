using System;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Controllers
{
    public class UserController : Controller
    {
        private readonly IDbService _dbService;

        public UserController(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _dbService.GetAllUsers();

            return View(users);
        }

        public async Task<IActionResult> AddUser(User user)
        {
            await _dbService.AddUser(new User
            {
                Email = user.Email,
                Id = Guid.NewGuid().ToString(),
                IsAdmin = false,
                Name = user.Name,
                Password = user.Password
            });

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(User user)
        {
           await _dbService.DeleteUser(user);

            return RedirectToAction("Index");
        }
    }
}
