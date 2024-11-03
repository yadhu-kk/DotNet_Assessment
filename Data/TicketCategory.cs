namespace EventTicketBookingApi.Data
{
    public class TicketCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public int TotalSeats { get; set; }
        public Event Event { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
    }
}
