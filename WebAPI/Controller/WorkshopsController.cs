using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.DTO;
using Persistence.Models;
using Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class WorkshopsController : ControllerBase
    {
        private readonly CTRService _service;

        public WorkshopsController(CTRService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ActionResult<IEnumerable<WorkshopDTO>> GetWorkshops()
        {
            return _service.GetWorkshops()
                .Select(workshop => (WorkshopDTO)workshop).ToList();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "administrator")]
        public ActionResult<IEnumerable<ReservationDTO>> GetWorkshopReservationsByID(Int32 id)
        {
            try
            {
                return _service.GetReservationsByWorkshopID(id)
                    .Select(reservation => (ReservationDTO)reservation).ToList(); ;
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(Roles = "administrator")]
        public IActionResult PostWorkshop([FromBody]WorkshopDTO workshop)
        {
            try
            {
                _service.CreateWorkshop((Workshop)workshop);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
