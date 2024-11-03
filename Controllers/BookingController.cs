using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventTicketBookingApi.Data;
using AutoMapper;
using EventTicketBookingApi.Service.IService;
using EventTicketBookingApi.Models;
using EventTicketBookingApi.Service;

namespace EventTicketBookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            this._bookingService = bookingService;
            this._mapper = mapper;
        }

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=212375
        //[HttpPost("{bookings}")]
        //public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto requestDto)
        //{
        //    if (requestDto == null)
        //    {
        //        return BadRequest("Booking data is null.");
        //    }

        //    try
        //    {
        //        var result = await _bookingService.CreateBookingAsync(requestDto);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }


        //}
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
        {
            try
            {
                var bookingResponse = await _bookingService.CreateBookingAsync(request);
                return Ok(bookingResponse); // 200 OK
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message }); // 400 Bad Request
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message }); // 404 Not Found
            }
            catch (DbUpdateException dbEx)
            {
                // Log exception details
                return StatusCode(500, new { error = "An error occurred while creating the booking. Please try again later." }); // 500 Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var cancellationResult = await _bookingService.CancelBookingAsync(id);
            return Ok(cancellationResult);
        }

    }
}

