using AutoMapper;
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
        private readonly IMapper _mapper;

        public HotelsController(ILogger<HotelsController> logger, IHttpContextAccessor httpContextAccessor, DataContext ctx, IMapper mapper)
        {
           
            _logger = logger;
            _htpp = httpContextAccessor.HttpContext;
            _ctx = ctx;    
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            var hotelsGet = _mapper.Map<List<HotelGetDto>>(hotels);

            return Ok(hotelsGet);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            if (hotel == null)
            {
                return NotFound();
            }

            var hotelGet = _mapper.Map<HotelGetDto>(hotel);
            return Ok(hotelGet);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreatedDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);

            _ctx.Hotels.Add(domainHotel); 
            await _ctx.SaveChangesAsync();

            var hotelGet = _mapper.Map<HotelGetDto>(domainHotel);

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, hotelGet );
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateHotel([FromBody] HotelCreatedDto update, int id)
        {
            var toUpdate = _mapper.Map<Hotel>(update);
            toUpdate.HotelId = id;

            _ctx.Hotels.Update(toUpdate);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);

            if(hotel == null)
            {
                return NotFound();
            }

            _ctx.Hotels.Remove(hotel);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }
    }
}
