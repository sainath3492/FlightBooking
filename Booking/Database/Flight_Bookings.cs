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
        public string EmailID { get; set; }

        public string Seat_Numbers { get; set; }
      

        public int Total_Seats { get; set; }

        public string Status { get; set; }


    }

    public class Passengers
    {
        [Key]
        public int PassengerID { get; set; }

        public int FK_BookingID { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }

        public int Age { get; set; }
    }
}
