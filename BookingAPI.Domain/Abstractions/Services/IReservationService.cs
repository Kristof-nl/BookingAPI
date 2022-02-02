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
        Task<Reservation> MakeReservationAsync(Reservation reservation);
        Task<List<Reservation>> GetAllReservationsAsync();
        Task<Reservation> GetReservationByIdAsync(int id);
        Task<Reservation> CancelReservationAsync(int id);
    }
}
