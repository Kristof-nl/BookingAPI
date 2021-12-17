using BookingAPI.Api.Dtos;
using BookingAPI.Dal;
using BookingAPI.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ILogger<HotelsController> _logger;
        private readonly HttpContext _htpp;
        private readonly DataContext _ctx;

        public HotelsController(ILogger<HotelsController> logger, IHttpContextAccessor httpContextAccessor, DataContext ctx)
        {
           
            _logger = logger;
            _htpp = httpContextAccessor.HttpContext;
            _ctx = ctx;    
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            return Ok(hotels);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreatedDto hotel)
        {
            var domainHotel = new Hotel();
            domainHotel.Name = hotel.Name;
            domainHotel.Address = hotel.Address;
            domainHotel.City = hotel.City;
            domainHotel.Country = hotel.Country;
            domainHotel.Description = hotel.Country;
            domainHotel.Stars = hotel.Stars;

            HotelGetDto hotelGet = new HotelGetDto();
            hotelGet.HotelId = domainHotel.HotelId;
            hotelGet.Name = domainHotel.Name;
            hotelGet.Address = domainHotel.Address;
            hotelGet.City = domainHotel.City;
            hotelGet.Country = domainHotel.Country;
            hotelGet.Stars = domainHotel.Stars;
            hotelGet.Description = domainHotel.Description;


            _ctx.Hotels.Add(domainHotel); 
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotelGet );
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel update, int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            hotel.Stars = update.Stars;
            hotel.Description = update.Description;
            hotel.Name = update.Name;

            _ctx.Hotels.Update(hotel);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            _ctx.Hotels.Remove(hotel);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
