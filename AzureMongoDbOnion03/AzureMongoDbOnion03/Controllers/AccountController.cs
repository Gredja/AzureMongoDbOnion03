using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Auntification;
using AzureMongoDbOnion03.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAunification _aunification;

        public AccountController(IAunification aunification)
        {
            _aunification = aunification;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid && user != null)
            {
                var regUser = await _aunification.TryLogin(user);
                await Authenticate(regUser.Name);

                if (regUser.IsAdmin)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //TODO
                }
            }

            return View("Index");
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
