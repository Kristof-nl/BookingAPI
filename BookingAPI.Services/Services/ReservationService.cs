using BookingAPI.Dal;
using BookingAPI.Domain.Abstractions.Repositories;
using BookingAPI.Domain.Abstractions.Services;
using BookingAPI.Domain.Models;
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
            //Step 1 - create reservation instance
            //var reservation = new Reservation
            //{
            //    HotelId = hotelId,
            //    RoomId = roomId,
            //    CheckInDate = checkIn,
            //    CheckOutDate = checkOut,
            //    Customer = customer
            //};

            //Step 2 Get the hotel, including room
            var hotel = await _hotelRepository.GetHotelByIdAsync(reservation.HotelId);

            //Step 3 Find the specifed room
            var room = hotel.Rooms.Where(r => r.RoomId == reservation.RoomId).FirstOrDefault();

            //Step 4 Make sure that the room is availible
            var isBusy = reservation.CheckInDate >= room.BusyFrom.Value
                && reservation.CheckOutDate <= room.BusyTo.Value;

            if (isBusy && room.NeedsRepair)
                return null;

            //Step 5 Set busyfrom and busyto on the room
            room.BusyFrom = reservation.CheckInDate;
            room.BusyTo = reservation.CheckOutDate;

            //Step 6 Persist all changes to the database
            _ctx.Rooms.Update(room);
            _ctx.Reservations.Add(reservation);

            await _ctx.SaveChangesAsync();

            return reservation;
        }
    }
}
