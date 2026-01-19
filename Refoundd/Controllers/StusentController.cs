using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refoundd.Models;
using System.Security.Cryptography;
using System.Text;

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
                .OrderByDescending(i => i.Date) // show latest items first
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
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new ProfileViewModel
            {
                UserName = user.User_Name,
                FirstName = user.First_Name,
                LastName = user.Last_Name,
                Email = user.Email,
            };
            return View(model);
        }


        // POST: Student/UpdateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProfile(ProfileViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                return View("Profile", model);
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // duplicate email check
            if (model.Email != user.Email && _context.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "This email is already in use");
                return View("Profile", model);
            }

            // duplicate username check
            if (model.UserName != user.User_Name && _context.Users.Any(u => u.User_Name == model.UserName))
            {
                ModelState.AddModelError("UserName", "This username is already taken");
                return View("Profile", model);
            }

            // update user info
            user.User_Name = model.UserName;
            user.First_Name = model.FirstName;
            user.Last_Name = model.LastName;
            user.Email = model.Email;
            _context.SaveChanges(); // save changes to database

            // update session data
            HttpContext.Session.SetString("UserName", user.User_Name);
            HttpContext.Session.SetString("UserEmail", user.Email);

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Profile");
        }


        // POST: Student/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill all password fields correctly";
                return RedirectToAction("Profile");
            }

            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // check current password
            string hashedCurrentPassword = HashPassword(model.CurrentPassword);
            if (user.Password != hashedCurrentPassword)
            {
                TempData["ErrorMessage"] = "Current password is incorrect";
                return RedirectToAction("Profile");
            }

            // check new password confirmation
            user.Password = HashPassword(model.NewPassword);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("Profile");
        }


        // Helper: Hash password using SHA256
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