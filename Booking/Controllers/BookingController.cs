﻿using Booking.Database;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("/api/v1.0/flight/booking/history/{emailid}"), AllowAnonymous]
      

        public IEnumerable<Flight_Bookings> GetFlightBookingsByEmail(string UserEmail )
        {
             var    data = _context.Flight_Booking.Where(u => (u.EmailID == UserEmail)).ToList();
            return data;
           
        }

        [HttpGet("/api/v1.0/flight/booking/ticket/{pnr}"), AllowAnonymous]
      
        public IEnumerable<Flight_Bookings> GetFlightBookingsByPNR(int PNR)
        {
            try
            {
                var data = _context.Flight_Booking.Where(u => (u.BookingID == PNR)).ToList();

                return data;

            }

            catch (Exception ex)
            {
                return null;
            }
          

        }
        


        [HttpPost("/api/v1.0/flight/booking"), AllowAnonymous]
     
        public int SaveBooking(Flight_Bookings flight_Bookings)
        {
            try
            {
                _context.Flight_Booking.Add(flight_Bookings);
                _context.SaveChanges();

                var data = _context.Flight_Booking.ToList().LastOrDefault();
                return data.BookingID;
            }

            catch (Exception ex)
            {
                return 0;
            }
          

        }

        [HttpPost("/api/v1.0/flight/Passengers"), AllowAnonymous]

        public Boolean SavePassengers(Passengers passengers)
        {
            try
            {
                _context.Passenger_Details.Add(passengers);
                _context.SaveChanges();
              
                return true;

            }

            catch (Exception ex)
            {
                return false;
            }
           
        }

        [HttpDelete("/api/v1.0/flight/booking/cancel/{pnr}"), AllowAnonymous]
      
        public int CancelBooking(int PNR)
        {
            try
            {
                var entity = _context.Flight_Booking.Find(PNR);
                entity.Status = "Cancelled";
                _context.SaveChanges();
                return PNR;

            }

            catch (Exception ex)
            {
                return 0;
            }

          

        }


    }

}
