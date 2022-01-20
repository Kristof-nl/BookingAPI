using BookingAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Domain.Abstractions.Services
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservation(int hotelId, int roomId, DateTime checkIn, DateTime checkOut, string customer);
    }
}
