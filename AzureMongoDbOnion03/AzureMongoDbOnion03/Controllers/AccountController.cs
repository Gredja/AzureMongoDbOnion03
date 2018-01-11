using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Aunification;
using AzureMongoDbOnion03.Application.Services.Models;
using AzureMongoDbOnion03.Domain;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
using AzureMongoDbOnion03.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

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

                    if (regUser.RoleId == (int)Roles.Admin)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return RedirectToAction("Index", "UsersCredit", new { userId = regUser.ForeignId });
                }

                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }

            return View("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(Roles), user.RoleId))
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
