using Microsoft.AspNetCore.Mvc;

namespace AzureMongoDbOnion03.Controllers
{
    public class UsersCreditController : Controller
    {
        public UsersCreditController()
        {
            //TODO
        }

        public IActionResult Index(string user)
        {
            
            return View();
        }
    }
}
