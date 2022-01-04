using AutoMapper;
using BookingAPI.Api.Dtos;
using BookingAPI.Domain.Models;

namespace BookingAPI.Api.AutoMapper
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<HotelCreatedDto, Hotel>();
            CreateMap<Hotel, HotelGetDto>();

        }
    }
}
