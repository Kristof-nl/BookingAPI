using AutoMapper;
using BookingAPI.Api.Dtos;
using BookingAPI.Domain.Models;

namespace BookingAPI.Api.AutoMapper
{
    public class RoomMappingProfiles : Profile
    {
        public RoomMappingProfiles()
        {
            CreateMap<Room, RoomGetDto>();
            CreateMap<RoomPostPutDto, Room>();
        }
    }
}
