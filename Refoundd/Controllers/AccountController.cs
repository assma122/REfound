using Microsoft.AspNetCore.Mvc;
using Refoundd.Models;  
using System.Text;
using System.Security.Cryptography;

namespace Refoundd.Controllers
{
    public class AccountController : Controller
    {
        private readonly RefoundContext _context;

        public AccountController(RefoundContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = HashPassword(model.Password);

                var user = _context.Users.FirstOrDefault(u =>
                    u.Email == model.Email && u.Password == hashedPassword);

                if (user != null)
                {
                    HttpContext.Session.SetInt32("UserId", user.User_Id);
                    HttpContext.Session.SetString("UserName", user.User_Name);
                    HttpContext.Session.SetString("UserEmail", user.Email);

                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Dashboard", "Student");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return View(model);
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered");
                    return View(model);
                }

                if (_context.Users.Any(u => u.User_Name == model.UserName))
                {
                    ModelState.AddModelError("UserName", "This username is already taken");
                    return View(model);
                }

                var user = new User
                {
                    User_Name = model.UserName,
                    First_Name = model.FirstName,
                    Last_Name = model.LastName,
                    Email = model.Email,
                    Password = HashPassword(model.Password),
                    Flag = 0
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Registration successful! Please login.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "You have been logged out successfully";
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
