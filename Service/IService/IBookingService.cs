using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;

namespace EventTicketBookingApi.Service.IService
{
    public interface IBookingService
    {
        Task<BookingResponseDto> CreateBookingAsync(BookingRequestDto request);
        Task<CancelBookingResponseDto> CancelBookingAsync(int bookingId);
        //Task<bool> ValidateBookingAsync(BookingRequestDto booking);
        //Task CreateBookingAsync(Booking booking);
    }
}
