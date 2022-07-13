using AirlineServices.Database;
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

        [HttpGet]
        [Route("/Airline_Master")]

        public IEnumerable<Airline> GetAirline_Master()
        {
            var data = _context.Airline_Master.ToList();
            return data;

        }

        [HttpPost]
        [Route("/SaveAirline")]
        public int SaveAirline(Airline flight_Bookings)
        {
            _context.Airline_Master.Add(flight_Bookings);
            _context.SaveChanges();
            var data = _context.Airline_Master.ToList().LastOrDefault();
            return data.AirlineID;

        }
    }
}
