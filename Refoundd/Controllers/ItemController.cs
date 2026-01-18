using Microsoft.AspNetCore.Mvc;
using Refoundd.Models;

namespace Refoundd.Controllers
{
    public class ItemController : Controller
    {
        private readonly RefoundContext _context;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Item item)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }


            ModelState.Remove("Date");
            ModelState.Remove("User");


            // item.Date = DateTime.Now;

            if (ModelState.IsValid)
            {
                item.User_Id = userId.Value;

                _context.Items.Add(item);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Item added successfully!";
                return RedirectToAction("Dashboard", "Student");
            }

            return View(item);
        }

        // GET: Item/Edit/5
        public IActionResult Edit(int id)
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

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var existingItem = _context.Items.Find(item.Item_Id);
                if (existingItem == null || existingItem.User_Id != userId)
                {
                    TempData["ErrorMessage"] = "Item not found or you don't have permission";
                    return RedirectToAction("Dashboard", "Student");
                }

                existingItem.Item_Name = item.Item_Name;
                existingItem.Description = item.Description;
                existingItem.Status = item.Status;
                existingItem.Location = item.Location;

                _context.SaveChanges();

                TempData["SuccessMessage"] = "Item updated successfully!";
                return RedirectToAction("Dashboard", "Student");
            }

            return View(item);
        }

        // GET: Item/Delete/5
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

        // POST: Item/DeleteConfirmed/5
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
                .OrderByDescending(i => i.Date)
                .ToList();

            ViewBag.SearchQuery = query;
            ViewBag.ResultCount = results.Count;

            return View(results);
        }

        // GET: Item/Details/5
        public IActionResult Details(int id)
        {
            var item = _context.Items.Find(id);
            if (item == null)
            {
                TempData["ErrorMessage"] = "Item not found";
                return RedirectToAction("Dashboard", "Student");
            }

            return View(item);
        }
    }
}