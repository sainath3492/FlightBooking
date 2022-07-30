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
        public int PK_BookingID { get; set; }
        
        public string Flight_ID { get; set; }
        public string Airline_Name { get; set; }
       
        public string From_Place { get; set; }
        public string To_Place { get; set; }
        public string Total_Price { get; set; }
        public string Onward_Price { get; set; }
        public string Return_Price { get; set; }
        public int Total_BusinessSeats { get; set; }
        public int Total_NonBusinessSeats { get; set; }

        public string EmailID { get; set; }
      

        public string PNR { get; set; }

        public string Trip_Type { get; set; }

        public DateTime Onward_JourneyDate { get; set; }

        public DateTime? Return_JourneyDate { get; set; }

        public string Status { get; set; }


    }

    public class Passengers
    {
        [Key]
        public int PK_PassengerID { get; set; }

        public int FK_BookingID { get; set; }
        public string PNR { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }
        public int Age { get; set; }
        public string Seats { get; set; }
        public string Class { get; set; }
        public string Meal { get; set; }
    }

    public class Discounts
    {
        [Key]
        public int DiscoundID { get; set; }

      
        public string Code { get; set; }

        public string Discount_Amount { get; set; }

       
    }
}
