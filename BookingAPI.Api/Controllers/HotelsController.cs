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
        private readonly ILogger<HotelsController> _logger;
        private readonly HttpContext _htpp;

        public HotelsController(ILogger<HotelsController> logger, IHttpContextAccessor httpContextAccessor)
        {
           
            _logger = logger;
            _htpp = httpContextAccessor.HttpContext;
        }

        [HttpGet]
        public IActionResult GetAllHotels()
        {
            HttpContext.Request.Headers.TryGetValue("my-middelware-header", out var headerDate);
            return Ok(headerDate);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetHotelById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateHotel([FromBody] Hotel update, int id)
        {
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteHotel(int id)
        {
            return NoContent();
        }
    }
}
