using EventTicketBookingApi.Data;
using hotelListing.API.Contract;

namespace EventTicketBookingApi.Contract
{
    public interface IBookingRepository:IGenericRepository<Booking>
    {
        Task<Booking> GetBookingWithEventAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsByEventAsync(int eventId);
        Task<IEnumerable<Booking>> GetBookingsByCustomerEmailAsync(string email);
        Task<bool> HasReachedBookingLimitAsync(string email, int eventId);
    }
}
