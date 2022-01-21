using AutoMapper;
using BookingAPI.Api.Dtos;
using BookingAPI.Domain.Models;

namespace BookingAPI.Api.AutoMapper
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<ReservationPutPostDto, Reservation>();
        }
    }
}
