using Microsoft.AspNetCore.Mvc;
using Refoundd.Models;

namespace Refoundd.Controllers
{
    public class ItemController : Controller
    {
        // Connect to Database
        private readonly RefoundContext _context;

        // Constructor to initialize the database context || Dependency Injection
        public ItemController(RefoundContext context)
        {
            _context = context;
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) 
            {
                TempData["ErrorMessage"] = "Please login first";
                return RedirectToAction("Login", "Account");
            }
            return View();
        }


        // POST: Item/Create
        [HttpPost] // action method only responds to POST requests
        [ValidateAntiForgeryToken] // prevent CSRF attacks
        public IActionResult Create(Item item)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            //ModelState.Remove("Date");
            //ModelState.Remove("User");
            // item.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                item.User_Id = userId.Value;
                _context.Items.Add(item); // add to DB  (in-memory)
                _context.SaveChanges(); // persist changes to DB

                TempData["SuccessMessage"] = "Item added successfully!";
                return RedirectToAction("Dashboard", "Student");
            }
            return View(item);
        }


        // GET: Item/Delete/ show confirmation page
        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var item = _context.Items.Find(id);
            if (item == null || item.User_Id != userId)
            {
                TempData["ErrorMessage"] = "Item not found or you don't have permission";
                return RedirectToAction("Dashboard", "Student");
            }
            return View(item);
        }

        // POST: Item/DeleteConfirmed/ delete the item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var item = _context.Items.Find(id);
            if (item == null || item.User_Id != userId)
            {
                TempData["ErrorMessage"] = "Item not found or you don't have permission";
                return RedirectToAction("Dashboard", "Student");
            }

            _context.Items.Remove(item);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Item deleted successfully!";
            return RedirectToAction("Dashboard", "Student");
        }


        // GET: Item/Search
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                TempData["ErrorMessage"] = "Please enter a search term";
                return RedirectToAction("Dashboard", "Student");
            }

            var results = _context.Items
                .Where(i =>
                    i.Item_Name.Contains(query) ||
                    i.Description.Contains(query) ||
                    i.Location.Contains(query))
                .OrderByDescending(i => i.Date) // show recent items first
                .ToList(); // execute the query as a list

            ViewBag.SearchQuery = query;
            ViewBag.ResultCount = results.Count;

            return View(results);
        }
    }
}