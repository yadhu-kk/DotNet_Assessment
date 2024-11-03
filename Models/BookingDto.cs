namespace EventTicketBookingApi.Models
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int CategoryId { get; set; }
        public TicketCategoryDto Category { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime BookingTime { get; set; }
        public string? QRCode { get; set; }
    }
}
