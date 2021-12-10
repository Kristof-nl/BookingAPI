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
        public HotelsController()
        {

        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            var hotels = GetHotels();
            return Ok(hotels);
        }

        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            var hotels = GetHotels();
            var hotel = hotels.FirstOrDefault(h => h.HotelId == id);

            if (hotel == null)
                return NotFound();

            return Ok(hotel);
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            var hotels = GetHotels();
            hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.HotelId }, hotel);
        }

        private List<Hotel> GetHotels()
        {
            return new List<Hotel>
            {
                new Hotel
                {
                    HotelId = 1,
                    Name = "Conquiscador",
                    Stars = 3,
                    Country = "Puerto Rico ",
                    City = "Fajardo",
                    Description = "Some nice description"
                },

                new Hotel
                {
                    HotelId = 1,
                    Stars = 3,
                    Name = "The Westin",
                    Country = "USA ",
                    City = "San Francisco",
                    Description = "Some nice description"
                }
            };
        }
    }
}
