using Microsoft.AspNetCore.Mvc;

namespace Refoundd.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
