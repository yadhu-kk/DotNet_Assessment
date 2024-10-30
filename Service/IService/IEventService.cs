using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;

namespace EventTicketBookingApi.Service.IService
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEventsAsync(EventFilterDto filter);
        Task<Event> GetEventByIdAsync(int id);
        //Task<EventStatistics> GetEventStatisticsAsync(int eventId);
    }
}
