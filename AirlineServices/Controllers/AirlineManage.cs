using AirlineServices.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineManage : ControllerBase
    {
        DatabaseContext _context;

        public AirlineManage(DatabaseContext context)
        {
            _context = context;
        }

       

        [HttpGet,AllowAnonymous]
     

        public IEnumerable<Airline> Airline_Search(DateTime? StartDate,string FromPlace, string ToPlace, string TripType)
        {
           

            try
            {
                var data = _context.Airline_Master.Where(u => (u.FlightStatus == "Active")).ToList();
                if (StartDate != null)
                {
                    data = data.Where(u => (u.Start_Date == StartDate)).ToList();
                }

                if (FromPlace != "" && FromPlace != null)
                {
                    data = data.Where(u => (u.From_Place == FromPlace)).ToList();
                }

                if (ToPlace != "" && ToPlace != null)
                {
                    data = data.Where(u => (u.To_Place == ToPlace)).ToList();
                }

                if (TripType != "" && TripType != null)
                {
                    data = data.Where(u => (u.TripType == TripType)).ToList();
                }
                return data;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }

        }

        [HttpPost("/api/v1.0/flight/airline/register"), Authorize(Roles = "Admin")]
       
        public int register(Airline flight_Bookings)
        {
           

            try
            {
                _context.Airline_Master.Add(flight_Bookings);
                _context.SaveChanges();
                var data = _context.Airline_Master.ToList().LastOrDefault();
                return data.AirlineID;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return 0;
            }

        }

        [HttpPost("/api/v1.0/flight/airline/inventory/add"), Authorize(Roles = "Admin")]
    
        public int add(Airline flight_Bookings)
        {

            try {
                var entity = _context.Airline_Master.Find(flight_Bookings.AirlineID);

                entity.From_Place = flight_Bookings.From_Place;
                entity.To_Place = flight_Bookings.To_Place;
                entity.Start_Date = flight_Bookings.Start_Date;
                entity.End_Date = flight_Bookings.End_Date;
                entity.Scheduled_Days = flight_Bookings.Scheduled_Days;
                entity.Instrument = flight_Bookings.Instrument;
                entity.Business_Seats = flight_Bookings.Business_Seats;
                entity.Non_Business_Seats = flight_Bookings.Non_Business_Seats;
                entity.Ticket_Cost = flight_Bookings.Ticket_Cost;
                entity.Rows = flight_Bookings.Rows;
                entity.Meal = flight_Bookings.Meal;
                _context.SaveChanges();
                return flight_Bookings.AirlineID;
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                return 0;
            }
           

        }

        [HttpPost("/api/v1.0/flight/airline/BlockAirline"), Authorize(Roles = "Admin")]
     
        public int BlockAirline(Status status)
        {
            try
            {
                var entity = _context.Airline_Master.Find(status.AirlineID);

                entity.FlightStatus = status.StatusValue;

                _context.SaveChanges();
                return status.AirlineID;

            }
            catch(Exception ex)
            {
                string error = ex.Message;
                return 0;
            }

        }
    }
}
