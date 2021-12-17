using AutoMapper;
using BookingAPI.Api.Dtos;
using BookingAPI.Domain.Models;

namespace BookingAPI.Api.AutoMapper
{
    public class HotelMappingsProfile : Profile
    {
        public HotelMappingsProfile()
        {
            CreateMap<HotelCreatedDto, Hotel>();
            CreateMap<Hotel, HotelGetDto>();

        }
    }
}
