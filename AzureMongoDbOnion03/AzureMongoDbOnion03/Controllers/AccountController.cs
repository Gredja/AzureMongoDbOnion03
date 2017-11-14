using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.Helpers;
using AzureMongoDbOnion03.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AzureMongoDbOnion03.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAunification _aunification;
        private readonly IDbService _dbService;

        public AccountController(IAunification aunification)
        {
            _aunification = aunification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AunificatedUser aunificatedUser)
        {
            if (ModelState.IsValid && aunificatedUser != null)
            {
                
                var regUser = await _aunification.TryLogin(aunificatedUser);

                if (regUser != null)
                {
                    await Authenticate(regUser);

                    if (regUser.Role.Name == Roles.Admin)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return RedirectToAction("Index", "UsersCredit", new { userId = regUser.ForeignId });
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View("Index");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Role?.Name.ToString())
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
