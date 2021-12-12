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
        private readonly DataSource _dataSource;
        public HotelsController(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var hotels = _dataSource.Hotels;
            return Ok(hotels);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = _dataSource.Hotels;
            var hotel = hotels.FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = _dataSource.Hotels;
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel update, int id)
        {
            var hotels = _dataSource.Hotels;
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
            var hotels = _dataSource.Hotels;
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
