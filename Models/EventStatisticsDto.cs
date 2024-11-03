namespace EventTicketBookingApi.Models
{
    public class EventStatisticsDto
    {
        public string EventName { get; set; }
        public int TotalCapacity { get; set; }
        public int TicketsSold { get; set; }
        public decimal Revenue { get; set; }
        public List<CategoryBreakdownDto> CategoryBreakdown { get; set; }
    }

}
