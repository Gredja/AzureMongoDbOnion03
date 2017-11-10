using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Application.Services.Auntification;
using AzureMongoDbOnion03.Application.Services.Auntification.Model;
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
        public async Task<IActionResult> Login(AunificatedUser aunificatedUser)
        {
            if (ModelState.IsValid && aunificatedUser != null)
            {
                var regUser = await _aunification.TryLogin(aunificatedUser);

                if (regUser != null)
                {
                    await Authenticate(regUser.Email);

                    if (regUser.IsAdmin)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //TODO
                    }
                }
            }

            return View("Index");
        }

        private async Task Authenticate(string email)
        {
            // создаем один claim
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, email));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }
}
