namespace EventTicketBookingApi.Data
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public List<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
