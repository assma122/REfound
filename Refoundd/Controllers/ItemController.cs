using Microsoft.AspNetCore.Mvc;
using Refoundd.Models;
using System.Linq;

namespace Refoundd.Controllers
{
    public class ItemController : Controller
    {
        private readonly RefoundContext _context;

        public ItemController(RefoundContext context)
        {
            _context = context;
        }

        // Display all items
        public IActionResult Index(string search)
        {
            var items = from i in _context.Items
                        select i;

            if (!string.IsNullOrEmpty(search))
            {
                items = items.Where(i => i.Item_Name.Contains(search) ||
                                         i.Description.Contains(search) ||
                                         i.Location.Contains(search));
            }

            return View(items.ToList());
        }

        /* ERROR (Title)
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index", "Home");
            }

            
            var results = _context.Items
                .Where(i => i.Title.Contains(query) ||
                            i.Description.Contains(query))
                .ToList();

            return View(results);
    }
        */
}
}
