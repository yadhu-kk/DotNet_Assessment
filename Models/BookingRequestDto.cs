namespace EventTicketBookingApi.Models
{
    public class BookingRequestDto
    {
        public int EventId { get; set; }
        public int CategoryId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int Quantity { get; set; }
        public string PromoCode { get; set; }
    }
}
