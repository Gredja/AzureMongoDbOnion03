using System.Security.Claims;
using System.Threading.Tasks;
using AzureMongoDbOnion03.Domain.Services.Services.DbServices;
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

        public AccountController(IAunification aunification, IDbService dbService)
        {
            _aunification = aunification;
            _dbService = dbService;
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

                    var debtor = await _dbService.GetDebtorById(regUser.ForeignId);
                    return RedirectToAction("Index", "UsersCredit", new { user = debtor.Name });
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
