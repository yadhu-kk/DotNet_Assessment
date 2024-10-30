using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;
using EventTicketBookingApi.Service;
using EventTicketBookingApi.Service.IService;

namespace EventTicketBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventTicketBookingDbContext _context;
        private readonly IEventService _eventsService;

        public EventsController(EventTicketBookingDbContext context, IEventService eventsService)
        {
            _context = context;
            _eventsService = eventsService;
        }



        // GET: api/Events/5
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEvents([FromQuery] EventFilterDto filter)
        {
            try
            {
                var result = await _eventsService.GetEventsAsync(filter);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }
    }
}
