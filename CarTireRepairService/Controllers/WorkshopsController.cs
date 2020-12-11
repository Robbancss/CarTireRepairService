using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using Persistence.Services;

namespace CarTireRepairService.Controllers
{
    [Authorize]
    public class WorkshopsController : Controller
    {
        private readonly CTRService _service;

        public WorkshopsController(CTRService service)
        {
            _service = service;
        }

        // GET: Workshops
        [AllowAnonymous]
        public IActionResult Index(string searchString = null)
        {
            ViewData["CurrentFilter"] = searchString;
            return View(_service.GetWorkshops(searchString));
        }

        public IActionResult Details(Int32 ID)
        {
            try
            {
                var workshop = _service.GetWorkShopByID(ID);
                return View(workshop);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
    }
}
