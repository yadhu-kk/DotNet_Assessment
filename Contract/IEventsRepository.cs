using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;
using EventTicketBookingApi.Repository;
using hotelListing.API.Contract;

namespace EventTicketBookingApi.Contract
{
    public interface IEventsRepository :IGenericRepository<Event>
    {
        Task<Event> GetEventWithCategoriesAsync(int id);
        Task<IEnumerable<Event>> GetUpcomingEventsAsync(EventFilterDto filter);
        //Task<EventStatistics> GetEventStatisticsAsync(int eventId);
        Task UpdateAvailableSeatsAsync(int eventId, int categoryId, int quantity);
    }
}
