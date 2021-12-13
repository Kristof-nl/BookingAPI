using BookingAPI.Api.Services;
using BookingAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : Controller
    {
        private readonly MyFirstService _myFirstService;
        public HotelsController(MyFirstService service)
        {
            _myFirstService = service;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var hotels = _myFirstService.GetHotels();
            return Ok(hotels);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _myFirstService.GetHotels();
            var hotel = hotels.FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = _myFirstService.GetHotels();
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel update, int id)
        {
            var hotels = _myFirstService.GetHotels();
            var old = hotels.FirstOrDefault(h => h.HotelId == id);

            if (old == null)
                return NotFound("No resource with te corresponding Id found");

            hotels.Remove(old);
            hotels.Add(update);

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            var hotels = _myFirstService.GetHotels();
            var toDelete = hotels.FirstOrDefault(h => h.HotelId == id);

            if(toDelete == null)
            {
                return NotFound("No resource with te corresponding Id found");
            }

            hotels.Remove(toDelete);

            return NoContent();
        }
    }
}
