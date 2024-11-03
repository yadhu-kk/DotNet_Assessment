using EventTicketBookingApi.Constant;
using EventTicketBookingApi.Contract;
using EventTicketBookingApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EventTicketBookingApi.Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly EventTicketBookingDbContext _dbContext;
        public BookingRepository(EventTicketBookingDbContext context) : base(context)
        {
           this._dbContext = context;
        }
        public async Task<Booking> GetBookingWithEventAsync(int id)
        {
            return await _dbContext.Bookings
                .Include(b => b.Event)
                .ThenInclude(e => e.TicketCategories)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        async Task<IEnumerable<Booking>> GetBookingsByEventAsync(int eventId)
        {
            return await _dbContext.Bookings
                .Where(b=>b.EventId == eventId).ToListAsync();
        }
        async Task<IEnumerable<Booking>> GetBookingsByCustomerEmailAsync(string email)
        {
            return await _dbContext.Bookings
                .Include (b => b.Event).Where(b=>b.CustomerEmail == email).ToListAsync();
        }
        public async Task<bool> HasReachedBookingLimitAsync(string email, int eventId)
        {
            var totalTickets = await _dbContext.Bookings
                .Where(b => b.CustomerEmail == email && b.EventId == eventId && b.Status == BookingStatus.ACTIVE)
                .SumAsync(b => b.Quantity);

            return totalTickets >= 6;
        }
        //public async Task<Booking> AddAsync(Booking booking)
        //{
        //    await _dbContext.AddAsync(booking);
        //    await _dbContext.SaveChangesAsync();
        //    return booking;
        //}
        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            return await _dbContext.Bookings
                .Include(b => b.Event)
                .ThenInclude(e => e.TicketCategories)
                .FirstOrDefaultAsync(b => b.Id == bookingId);
        }
        public async Task CancelBookingAsync(Booking booking)
        {
            booking.Status = BookingStatus.CANCELLED;
            booking.CancellationDate = DateTime.UtcNow;
            var category = await _dbContext.Events
                .Include(e => e.TicketCategories)
                .Where(e => e.Id == booking.EventId)
                .SelectMany(e => e.TicketCategories)
                .Where(tc => tc.Id == booking.CategoryId)
                .FirstOrDefaultAsync();

            if (category != null)
            {
                category.AvailableSeats += booking.Quantity;
                _dbContext.TicketCategories.Update(category);
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
