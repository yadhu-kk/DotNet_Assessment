using AutoMapper;
using hotelListing.API.Data;
using hotelListing.API.Models.Country;
using hotelListing.API.Models.Hotel;

namespace hotelListing.API.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<>().ReverseMap();
            
        }
    }
}
