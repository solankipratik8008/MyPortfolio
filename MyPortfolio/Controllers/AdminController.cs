using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MyPortfolio.Models;
using System.Linq;

namespace MyPortfolio.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private const string ADMIN_USER = "OMU";
        private const string ADMIN_PASS = "98981096";

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Admin/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == ADMIN_USER && password == ADMIN_PASS)
            {
                HttpContext.Session.SetString("IsAdmin", "true");
                return RedirectToAction("ManageComments");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        // LOGOUT
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("IsAdmin") == "true";
        }

        public IActionResult ManageComments()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var comments = _context.Comments.OrderByDescending(c => c.DatePosted).ToList();
            return View(comments);
        }

        public IActionResult EditComment(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var comment = _context.Comments.FirstOrDefault(c => c.Id == id);
            return View(comment);
        }

        [HttpPost]
        public IActionResult EditComment(Comment updatedComment)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var comment = _context.Comments.FirstOrDefault(c => c.Id == updatedComment.Id);
            if (comment != null)
            {
                comment.Name = updatedComment.Name;
                comment.Message = updatedComment.Message;
                _context.SaveChanges();
                return RedirectToAction("ManageComments");
            }

            return View(updatedComment);
        }

        public IActionResult DeleteComment(int id)
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login");

            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageComments");
        }
    }
}
