using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarTireRepairService.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.Models;
using Persistence.Services;

namespace CarTireRepairService.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Client> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly CTRService _service;

        public HomeController(ILogger<HomeController> logger, UserManager<Client> userManager, CTRService service)
        {
            _logger = logger;
            _userManager = userManager;
            _service = service;
        }

        public IActionResult Index()
        {
            var userID = _userManager.GetUserId(User);
            if (userID != null)
            {
                var userReservations = _service.GetReservationsByUserID(Int32.Parse(userID));
                ViewBag.UserReservations = userReservations;
                return View(userReservations);
            }
            return View();
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
