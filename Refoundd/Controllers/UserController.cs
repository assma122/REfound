using Microsoft.AspNetCore.Mvc;
using Refoundd.Models;
using System.Linq;

namespace Refoundd.Controllers
{
    public class UserController : Controller
    {
        private readonly RefoundContext context;

        public UserController(RefoundContext ctx)
        {
            context = ctx;
        }

        // 1. Display all users
        public IActionResult Index()
        {
            var allUsers = context.Users.ToList(); // تجيب كل المستخدمين
            return View(allUsers);
        }

        // 2. Search users by name (partial match)
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return View("Index", context.Users.ToList());
            }

            var results = context.Users
                                 .Where(u => u.User_Name.Contains(searchTerm))
                                 .ToList();
            return View("Index", results); // نعرض النتائج في نفس الـ Index
        }
    }
}
