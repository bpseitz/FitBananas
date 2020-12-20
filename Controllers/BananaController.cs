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
    public class BananaController : Controller
    {
        
        // private readonly DbContext _context;

        // public BananaController(MyContext context)
        // {
        //     _context = context;
        // }

        [HttpGet("home")]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View();
        }

        [HttpGet("settings")]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            return RedirectToAction("Index", "Home");
        }
      
    }
}