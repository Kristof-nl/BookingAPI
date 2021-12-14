using BookingAPI.Api.Services;
using BookingAPI.Api.Services.Abstractions;
using BookingAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ISingletonOperation _singleton;
        private readonly ITransientOperation _transient;
        private readonly IScopedOperation _scoped;
        private readonly ILogger<HotelsController> _logger;


        public HotelsController(MyFirstService service, ISingletonOperation singleton, 
            ITransientOperation transient, IScopedOperation scoped, ILogger<HotelsController> logger)
        {
            _myFirstService = service;
            _singleton = singleton;
            _transient = transient;
            _scoped = scoped;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            _logger.LogInformation($"GUID of singleton: {_singleton.Guid} ");
            _logger.LogInformation($"GUID of trnsient: {_transient.Guid} ");
            _logger.LogInformation($"GUID of scoped: {_scoped.Guid} ");

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
