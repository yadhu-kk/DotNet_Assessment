namespace EventTicketBookingApi.Models
{
    public class BookingResponseDto
    {
        public int BookingId { get; set; }
        public int EventId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public double RefundAmount { get; set; }
        public DateTime? CancellationDate { get; set; }
    }
}
