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
        [Route("/GetFlightBookingsByEmail")]

        public IEnumerable<Flight_Bookings> GetFlightBookingsByEmail(string UserEmail )
        {
             var    data = _context.Flight_Booking.Where(u => (u.EmailID == UserEmail)).ToList();
            return data;
           
        }

        [HttpGet]
        [Route("/GetFlightBookingsByPNR")]
        public IEnumerable<Flight_Bookings> GetFlightBookingsByPNR( int PNR)
        {

            var data = _context.Flight_Booking.Where(u => (u.BookingID == PNR)).ToList();

            return data;
         
        }



        [HttpPost]
        [Route("/SaveBooking")]
        public int SaveBooking (Flight_Bookings flight_Bookings)
        {
            _context.Flight_Booking.Add(flight_Bookings);
            _context.SaveChanges();
            var data = _context.Flight_Booking.ToList().LastOrDefault();
            return data.BookingID;

        }

        [HttpPost]
        [Route("/CancelBooking")]
        public int CancelBooking(int PNR)
        {
            var entity = _context.Flight_Booking.Find(PNR);
            entity.Status = "Cancelled";
            _context.SaveChanges();
            return PNR;

        }


    }

}
