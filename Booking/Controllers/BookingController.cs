using Booking.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        DatabaseContext _context;

        public BookingController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
      
        public IEnumerable<Flight_Bookings> GetFlightBookings()
        {
            return _context.Flight_Booking.ToList();
        }

        [HttpPost]

        public int SaveBooking (Flight_Bookings flight_Bookings)
        {
           
         
          
            _context.Flight_Booking.Add(flight_Bookings);
         
            _context.SaveChanges();
            var data = _context.Flight_Booking.ToList().LastOrDefault();


            return data.BookingID;

        }


    }

}
