using Booking.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

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

        [HttpGet("/api/v1.0/flight/booking/history"), AllowAnonymous]
      

        public IEnumerable<Flight_Bookings> GetFlightBookingsByEmail(string UserEmail )
        {
             var    data = _context.Flight_Booking.Where(u => (u.EmailID == UserEmail)).ToList();

           
            return data;
           
        }

        [HttpPost("/api/v1.0/flight/booking/AddDiscounts"), AllowAnonymous]


        public Discounts AddDiscounts(Discounts discounts)
        {
            _context.Discount_Details.Add(discounts);
            _context.SaveChanges();
            var data = _context.Discount_Details.ToList().LastOrDefault();
            return data;

        }

        [HttpGet("/api/v1.0/flight/booking/GetDiscounts"), AllowAnonymous]


        public IEnumerable<Discounts> GetDiscounts(string Code)
        {
            var data = _context.Discount_Details.ToList();
            string test = Code;
            if (Code!="" && Code != null )
            {
                 data = _context.Discount_Details.Where(u => (u.Code == Code)).ToList();
            }
          
         
            return data;

        }

        [HttpGet("/api/v1.0/flight/booking/ticket/{pnr}"), AllowAnonymous]
      
        public IEnumerable<Flight_Bookings> GetFlightBookingsByPNR(string PNR)
        {
            try
            {
                var data = _context.Flight_Booking.Where(u => (u.PNR == PNR)).ToList();

                return data;

            }

            catch (Exception ex)
            {
                return null;
            }
          

        }

        [HttpGet("/api/v1.0/flight/booking/GetPassengerDetails/{BookingID}"), AllowAnonymous]


        public IEnumerable<Passengers> GetPassengerDetails(int BookingID)
        {
            var data = _context.Passenger_Details.Where(u => (u.FK_BookingID == BookingID)).ToList();
            return data;

        }


        [HttpPost("/api/v1.0/flight/booking"), AllowAnonymous]
     
        public Flight_Bookings SaveBooking(Flight_Bookings flight_Bookings)
        {
            try
            {
                var pnr = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Millisecond.ToString();
                flight_Bookings.PNR = pnr;
                _context.Flight_Booking.Add(flight_Bookings);
                _context.SaveChanges();

                var data = _context.Flight_Booking.ToList().LastOrDefault();

                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672")
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                QueueProducer.Publish(channel, flight_Bookings.Total_BusinessSeats, flight_Bookings.Total_NonBusinessSeats, flight_Bookings.Flight_ID);

                return data;
            }

            catch (Exception ex)
            {
                return null;
            }
          

        }

        public static void Publish(IModel channel, int businessseats, int normalseats)
        {
            channel.QueueDeclare("demo-queue2",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var count = 0;

            //while (true)
            //{
                var message = new { Name = "Producer", Message = $"{businessseats}"+"," + $"{normalseats}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "demo-queue2", null, body);
               
           // }
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
      
        public string CancelBooking(int PNR)
        {
            try
            {
                var entity = _context.Flight_Booking.Find(PNR);
                entity.Status = "Cancelled";
                _context.SaveChanges();
                return "Cancelled";

            }

            catch (Exception ex)
            {
                return "Internal Server Error";
            }

          

        }




    }

}
