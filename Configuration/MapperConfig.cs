using AutoMapper;
using EventTicketBookingApi.Data;
using EventTicketBookingApi.Models;

namespace hotelListing.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Booking, BookingRequestDto>().ReverseMap();
            CreateMap<Booking, BookingResponseDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Event, EventFilterDto>().ReverseMap();
            CreateMap<TicketCategory, TicketCategoryDto>().ReverseMap();
            CreateMap<Booking, CancelBookingResponseDto>().ReverseMap();
            CreateMap<Event, EventStatisticsDto>().ReverseMap();
            CreateMap<TicketCategory, CategoryBreakdownDto>().ReverseMap();

        }
    }
}
