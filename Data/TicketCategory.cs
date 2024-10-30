namespace EventTicketBookingApi.Data
{
    public class TicketCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public Event Event { get; set; }
    }
}
