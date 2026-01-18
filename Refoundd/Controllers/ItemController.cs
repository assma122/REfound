using Microsoft.AspNetCore.Mvc;
using Refoundd.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Refoundd.Controllers
{
    public class ItemController : Controller
    {
        private readonly RefoundContext _context;

        public ItemController(RefoundContext context)
        {
            _context = context;
        }

        // GET: Item/Search
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                TempData["ErrorMessage"] = "Please enter a search term";
                return RedirectToAction("Dashboard", "Student");
            }

            // البحث في اسم العنصر، الوصف، والموقع
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

        // GET: Item/Filter (للـ Dashboard Filters)
        public IActionResult Filter(string status)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            IQueryable<Item> query = _context.Items;

            if (!string.IsNullOrEmpty(status) && status != "all")
            {
                query = query.Where(i => i.Status == status);
            }

            var items = query.OrderByDescending(i => i.Date).ToList();

            ViewBag.FilterStatus = status;
            return View("SearchResults", items);
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

        // form to add item
        public IActionResult Create()
        {
            return View();
        }
        // post : to save new item
        [HttpPost]
        public IActionResult Create(ItemController item)
        {
            if (ModelState.IsValid)
            {
                // it is optional to set date automatically
                //item.Date = DateTime.Now;
               // _context.Items.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        // here to Delete Item
        public IActionResult Delete(int id)
        {
            var item = _context.Items.Find(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}