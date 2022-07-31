using AirlineServices.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Update : ControllerBase
    {
        DatabaseContext _context;
        public Update(IServiceProvider serviceProvider)
        {

            _context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DatabaseContext>();
            
        }

        public string updateseats( int busineesseats,int nonbusineesseats,int flightid)
        {
            var entity = _context.Airline_Master.Find(flightid);

            entity.Business_Seats = entity.Business_Seats- busineesseats;
            entity.Non_Business_Seats = entity.Non_Business_Seats- nonbusineesseats;

            _context.SaveChanges();
            return "Updated Succesfully";
        }
    }
}
