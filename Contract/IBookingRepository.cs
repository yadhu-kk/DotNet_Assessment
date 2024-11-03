using EventTicketBookingApi.Data;
using hotelListing.API.Contract;

namespace EventTicketBookingApi.Contract
{
    public interface IBookingRepository:IGenericRepository<Booking>
    {
          Task<Booking> GetBookingWithEventAsync(int id);
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<bool> HasReachedBookingLimitAsync(string email, int eventId);
        Task CancelBookingAsync(Booking booking);


    }
}
