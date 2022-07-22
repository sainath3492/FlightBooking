using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Booking.Database
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext (DbContextOptions<DatabaseContext> options):base(options)
        {
            
        }
        public DbSet<Flight_Bookings> Flight_Booking { get; set; }

        public DbSet<Passengers> Passenger_Details { get; set; }

        public DbSet<Discounts> Discount_Details { get; set; }
    }
}
