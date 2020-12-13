using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class ReservationController : ControllerBase
    {
        private readonly CTRService _service;

        public ReservationController(CTRService service)
        {
            _service = service;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "administrator")]
        public IActionResult DeleteReservation(Int32 id)
        {
            if (_service.DeleteReservationByID(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
