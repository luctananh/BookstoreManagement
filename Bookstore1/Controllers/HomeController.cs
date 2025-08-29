using Bookstore1.Data;
using Bookstore1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bookstore1.Controllers
{
    public class HomeController : Controller
    {
        private readonly Bookstore1Context _context;

        public HomeController(Bookstore1Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Book.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
