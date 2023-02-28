using Inz.Data;
using Inz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Inz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cs()
        {
            return View();
        }

        public IActionResult Lol()
        {
            return View();
        }

        public IActionResult Fortnite()
        {
            return View();
        }
        public IActionResult PilkaNozna()
        {
            return View();
        }

        public IActionResult Koszykowka()
        {
            return View();
        }
        public IActionResult Siatkowka()
        {
            return View();
        }

        [Authorize]
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
