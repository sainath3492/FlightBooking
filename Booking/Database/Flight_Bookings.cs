using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Database
{
    public class Flight_Bookings
    {
        [Key]
        public int BookingID { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime Onward_JourneyDate { get; set; }
        public DateTime Return_JourneyDate { get; set; }
        public string Onward_Meal { get; set; }
        public string Return_Meal { get; set; }
        public string Onward_Price { get; set; }
        public string Return_Price { get; set; }
        public string Total_Price { get; set; }
        public string Trip_Type { get; set; }

   
    }
}
