using AutoMapper;
using EventTicketBookingApi.Constant;
using EventTicketBookingApi.Contract;
using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;
using EventTicketBookingApi.Repository;
using EventTicketBookingApi.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace EventTicketBookingApi.Service
{
    public class EventsService : IEventService
    {
        private readonly EventTicketBookingDbContext _dbContext;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventsRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventsService(EventTicketBookingDbContext context,IBookingRepository bookingRepository,
            IEventsRepository eventsRepository,IMapper mapper)
            
        {
            this._dbContext=context;
            this._bookingRepository=bookingRepository;
            this._eventRepository=eventsRepository;
            this._mapper=mapper;
        }

        public async Task<IEnumerable<EventDto>> GetUpcomingEventsAsync(EventFilterDto filter)
        {
            var eventEntities = await _eventRepository.GetUpcomingEventsAsync(filter);
            var eventDtos = _mapper.Map<IEnumerable<EventDto>>(eventEntities);
            return eventDtos;
        }
        public async Task<EventStatisticsDto> GetEventStatisticsAsync(int eventId)
        {
            var eventDetails = await _eventRepository.GetEventWithCategoriesAsync(eventId);

            int totalTicketsSold = eventDetails.Bookings.Count(b => b.Status == BookingStatus.ACTIVE);
            decimal totalRevenue = eventDetails.Bookings.Sum(b => b.TotalAmount);

            var categoryBreakdown = eventDetails.TicketCategories.Select(tc => new CategoryBreakdownDto
            {
                Name = tc.Name,
                SoldPercentage = (double)(tc.TotalSeats - tc.AvailableSeats) / tc.TotalSeats * 100,
                Revenue = (tc.TotalSeats - tc.AvailableSeats) * tc.Price
            }).ToList();
            Console.WriteLine($"Total Tickets Sold: {totalTicketsSold.ToString()}, Total Revenue: {totalRevenue}");


            return new EventStatisticsDto
            {
                EventName = eventDetails.Name,
                TotalCapacity = eventDetails.TicketCategories.Sum(tc => tc.TotalSeats),
                TicketsSold = totalTicketsSold,
                Revenue = totalRevenue,
                CategoryBreakdown = categoryBreakdown
            };
        }
    }
}
