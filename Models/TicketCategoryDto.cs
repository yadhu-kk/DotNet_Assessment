namespace EventTicketBookingApi.Models
{
    public class TicketCategoryDto
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int AvailableSeats { get; set; }
    }
}
