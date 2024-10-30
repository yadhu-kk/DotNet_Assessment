using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;

namespace EventTicketBookingApi.Service.IService
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(BookingRequestDto bookingRequest);
        Task CancelBookingAsync(int bookingId);
        Task<bool> ValidateBookingAsync(BookingRequestDto booking);
    }
}
