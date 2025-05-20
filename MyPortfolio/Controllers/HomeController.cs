using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Home Page with Latest 5 Comments
        public IActionResult Index()
        {
            var latestComments = _context.Comments
                .OrderByDescending(c => c.DatePosted)
                .Take(5)
                .ToList();

            ViewBag.LatestComments = latestComments;
            return View();
        }

        // About Page
        public IActionResult About()
        {
            return View();
        }

        // Projects Page
        public IActionResult Projects()
        {
            return View();
        }

        // Contact Page
        public IActionResult Contact()
        {
            ViewBag.Mobile = "548-384-8008";
            return View();
        }
    }
}