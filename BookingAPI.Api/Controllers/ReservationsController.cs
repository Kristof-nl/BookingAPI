using AutoMapper;
using BookingAPI.Api.Dtos;
using BookingAPI.Domain.Abstractions.Services;
using BookingAPI.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public ReservationsController(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationPutPostDto reservationDto)
        {
            var reservation = _mapper.Map<Reservation>(reservationDto);
            var result = await _reservationService.MakeReservationAsync(reservation);

            if (result == null)
                return BadRequest("Cannot make reservation");

            var mapped = _mapper.Map<ReservationGetDto>(result);

            return Ok(mapped);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservationsAsync()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();

            var mapped = _mapper.Map<List<ReservationGetDto>>(reservations);

            return Ok(mapped);
        }


        [HttpGet]
        [Route("{reservationId}")]
        public async Task<IActionResult> GetReservationById(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            if(reservation == null)
                return NotFound($"No reservation found for the id {reservationId}");

            var mapped = _mapper.Map<ReservationGetDto>(reservation);
            return Ok(mapped);
        }

        [HttpDelete]
        [Route("{reservationId}")]
        public async Task<IActionResult> CancelReservationById(int reservationId)
        {
            var deleted = await _reservationService.CancelReservationAsync(reservationId);

            if(deleted == null)
                return NotFound();

            return NoContent(); 
        }
    }
}
