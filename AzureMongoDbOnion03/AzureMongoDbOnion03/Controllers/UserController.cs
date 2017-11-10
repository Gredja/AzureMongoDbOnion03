using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.ViewModels;
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
            var debtors = await _dbService.GetAllDebtors();

            var usersPlusDebtorName = CreateUsersPlusName(users, debtors);

            var usersViewModel = new UsersViewModel
            {
                Debtors = debtors,
                UsersPlusDebtorName = usersPlusDebtorName,
                SelectedDebtorId = string.Empty
            };

            return View(usersViewModel);
        }

        public async Task<IActionResult> AddUser(UsersViewModel usersViewModel)
        {
            var debtors = await _dbService.GetAllDebtors();
            var users = await _dbService.GetAllUsers();

            if (users.Any(x => x.Email == usersViewModel.NewUser.Email))
            {
                var user = new User
                {
                    Email = usersViewModel.NewUser.Email,
                    Id = Guid.NewGuid().ToString(),
                    IsAdmin = false,
                    ForeignId = debtors.FirstOrDefault(x => x.Id == usersViewModel.SelectedDebtorId)?.Id,
                    Password = usersViewModel.NewUser.Password
                };

                await _dbService.AddUser(user);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteUser(User user)
        {
            await _dbService.DeleteUser(user);

            return RedirectToAction("Index");
        }

        private List<UserPlusDebtorName> CreateUsersPlusName(IEnumerable<User> users, IEnumerable<Debtor> debtors)
        {
            var usersPlusDebtorName = new List<UserPlusDebtorName>();

            foreach (var user in users)
            {
                usersPlusDebtorName.Add(new UserPlusDebtorName()
                {
                    Name = debtors?.FirstOrDefault(x => x.Id == user.ForeignId)?.Name,
                    User = user
                });
            }

            return usersPlusDebtorName;
        }
    }
}
