using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FitBananas.Models;

namespace FitBananas.Controllers
{
    public class HomeController : Controller
    {
        private readonly BananaContext _context;

        public HomeController(BananaContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            var modelView = new IndexViewModel(_context);
            return View(modelView);
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
