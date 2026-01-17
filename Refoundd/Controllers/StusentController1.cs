using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refoundd.Models;

namespace Refoundd.Controllers
{
    public class StudentController : Controller
    {
        private readonly RefoundContext _context;

        public StudentController(RefoundContext context)
        {
            _context = context;
        }

        // GET: Student/Dashboard
        public IActionResult Dashboard()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Please login first";
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Items = _context.Items
                .OrderByDescending(i => i.Date)
                .ToList();

            return View(user);
        }

        // GET: Student/Profile
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.Find(userId);
            return View(user);
        }

        // GET: Student/MyItems
        public IActionResult MyItems()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var myItems = _context.Items
                .Where(i => i.User_Id == userId)
                .OrderByDescending(i => i.Date)
                .ToList();

            return View(myItems);
        }
    }
}