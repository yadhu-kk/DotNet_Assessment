using System.ComponentModel.DataAnnotations;

namespace EventTicketBookingApi.Models
{
    public class BookingRequestDto
    {
        public int EventId { get; set; }
        public int CategoryId { get; set; }
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Range(1, 6, ErrorMessage = "You can book between 1 and 6 tickets.")]
        public int Quantity { get; set; }
        public string PromoCode { get; set; }
        public string PaymentToken { get; set; }
    }
}
