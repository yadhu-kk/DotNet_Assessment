using Microsoft.EntityFrameworkCore;

namespace EventTicketBookingApi.Data
{
    public class EventTicketBookingDbContext:DbContext
    {
        public EventTicketBookingDbContext(DbContextOptions options):base(options) 
        { 

        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
    
    }
}
