using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;

using System;
using System.Linq;
using System.Xml.Linq;

namespace Portfolio.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var comments = _context.Comments.OrderByDescending(c => c.DatePosted).ToList();
            return View(comments);
        }

        [HttpPost]
        public IActionResult Add(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.DatePosted = DateTime.Now;
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // In case of validation error
            var allComments = _context.Comments.OrderByDescending(c => c.DatePosted).ToList();
            return View("Index", allComments);
        }
    }
}
