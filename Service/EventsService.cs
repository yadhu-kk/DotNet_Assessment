using AutoMapper;
using EventTicketBookingApi.Contract;
using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;
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
        public Task<Event> GetEventByIdAsync(int id)
        {
           // throw new NotImplementedException();
           
            
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(EventFilterDto filter)
        {
            // throw new NotImplementedException();
            var query = _dbContext.Events.AsQueryable();
            if (filter.StartDate.HasValue) 
            { 
                query=query.Where(e=>e.DateTime >=filter.StartDate);
            }
            if (filter.EndDate.HasValue)
            {
                query=query.Where(e=>e.DateTime <=filter.EndDate);
            }
            if (!string.IsNullOrEmpty(filter.Category))
            {
                query = query.Where(e => e.TicketCategories.Any(tc => tc.Name == filter.Category));
            }
            if (filter.MinPrice.HasValue) 
            {
                query = query.Where(e => e.TicketCategories.Any(tc => tc.Price == filter.MinPrice));
            }
            if (filter.MaxPrice.HasValue) 
            {
                query = query.Where(e => e.TicketCategories.Any(tc => tc.Price == filter.MaxPrice));
            }
            return await query.Include(e => e.TicketCategories).ToListAsync();

        }
    }
}
