namespace EventTicketBookingApi.Models
{
    public class EventDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public List<TicketCategoryDto> Categories { get; set; }
        public int AvailableTickets { get; set; }

    }
}
