using BookingAPI.Dal;
using BookingAPI.Domain.Abstractions.Repositories;
using BookingAPI.Domain.Abstractions.Services;
using BookingAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IHotelsRepository _hotelRepository;
        private readonly DataContext _ctx;

        public ReservationService(IHotelsRepository hotelRepo, DataContext ctx)
        {
            _hotelRepository = hotelRepo;
            _ctx = ctx;
        }


        public async Task<Reservation> MakeReservation(Reservation reservation)
        {

            //Step 1 Get the hotel, including room
            var hotel = await _hotelRepository.GetHotelByIdAsync(reservation.HotelId);

            //Step 2 Find the specifed room
            var room = hotel.Rooms.Where(r => r.RoomId == reservation.RoomId).FirstOrDefault();

            if(hotel == null || room == null) return null;

            //Step 3 Make sure that the room is availible
            bool isBusy = await _ctx.Reservations.AnyAsync(r => 
                (reservation.CheckInDate >= r.CheckInDate && reservation.CheckInDate <= r.CheckOutDate)
                && (reservation.CheckOutDate >= r.CheckInDate && reservation.CheckOutDate <= r.CheckOutDate)
            );
                
            if (isBusy)
                return null;

            if (room.NeedsRepair)
                return null;


            //Step 4 Persist all changes to the database
            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);

            await _ctx.SaveChangesAsync();

            return reservation;
        }
    }
}
