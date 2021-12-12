using BookingAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Api
{
    public class DataSource
    {
        public DataSource()
        {
            Hotels = GetHotels();
        }

        public List<Hotel> Hotels { get; set; }
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
                    HotelId = 2,
                    Name = "The Westin",
                    Stars = 4,
                    Country = "USA ",
                    City = "San Francisco",
                    Description = "Some nice description"
                }
            };
        }
    }
}
