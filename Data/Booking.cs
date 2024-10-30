﻿using EventTicketBookingApi.Constant;

namespace EventTicketBookingApi.Data
{
    public class Booking
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public  int CategoryId {  get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateTime { get; set; }
        public string QRCode { get; set; }
        public BookingStatus Status { get; set; }
        public Event Event { get; set; }

    }
}