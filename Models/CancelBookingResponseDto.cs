namespace EventTicketBookingApi.Models
{
    public class CancelBookingResponseDto
    {
        public int BookingId { get; set; }
        public bool IsCancelled { get; set; }
        public double RefundAmount { get; set; }
        public DateTime CancellationDate { get; set; }
    }
}
