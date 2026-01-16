using Microsoft.AspNetCore.Mvc;

namespace Refoundd.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
