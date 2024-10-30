using EventTicketBookingApi.Contract;
using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventTicketBookingApi.Repository
{
    public class EventsRepository : GenericRepository<Event>, IEventsRepository
    {
        private readonly EventTicketBookingDbContext _dbContext;
        public EventsRepository(EventTicketBookingDbContext context) : base(context)
        {
           
            this._dbContext = context;
        }
        public async Task<Event> GetEventWithCategoriesAsync(int id)
        {
            return await _dbContext.Events
                .Include(e => e.TicketCategories)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync(EventFilterDto filter)
        {
            var query = _dbContext.Events
                .Include(e => e.TicketCategories)
                .Where(e => e.DateTime > DateTime.UtcNow);

            if (filter.StartDate.HasValue)
                query = query.Where(e => e.DateTime >= filter.StartDate);

            if (filter.EndDate.HasValue)
                query = query.Where(e => e.DateTime <= filter.EndDate);

            if (!string.IsNullOrEmpty(filter.Category))
                query = query.Where(e => e.TicketCategories.Any(tc => tc.Name == filter.Category));

            if (filter.MinPrice.HasValue)
                query = query.Where(e => e.TicketCategories.Any(tc => tc.Price >= filter.MinPrice));

            if (filter.MaxPrice.HasValue)
                query = query.Where(e => e.TicketCategories.Any(tc => tc.Price <= filter.MaxPrice));

            return await query.ToListAsync();
        }
        public async Task UpdateAvailableSeatsAsync(int eventId, int categoryId, int quantity)
        {
            var category = await _dbContext.TicketCategories
                .FirstOrDefaultAsync(tc => tc.Id == categoryId && tc.Event.Id == eventId);

            if (category != null)
            {
                category.AvailableSeats -= quantity;
                _dbContext.TicketCategories.Update(category);
            }
        }
    }
}
