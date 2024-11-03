using EventTicketBookingApi.Models;

namespace EventTicketBookingApi.Service.IService
{
    public interface IEventService
    {
        //Task<IEnumerable<Event>> GetEventsAsync(EventFilterDto filter);
        Task<IEnumerable<EventDto>> GetUpcomingEventsAsync(EventFilterDto filter);
        Task<EventStatisticsDto> GetEventStatisticsAsync(int eventId);
        //Task<Event> GetEventByIdAsync(int id);
        //Task<EventStatistics> GetEventStatisticsAsync(int eventId);
    }
}
