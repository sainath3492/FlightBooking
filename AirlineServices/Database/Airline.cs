using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineServices.Database
{
    public class Airline
    {
        [Key]
        public int AirlineID { get; set; }

        public string AirlineName { get; set; }

        public string From_Place { get; set; }

        public string To_Place { get; set; }


        public DateTime Start_Date { get; set; }

        public DateTime End_Date { get; set; }

        public string Scheduled_Days { get; set; }

        public string Instrument { get; set; }

        public int Business_Seats { get; set; }

        public int Non_Business_Seats { get; set; }

        public string Ticket_Cost { get; set; }

        public string Rows { get; set; }

        public string Meal { get; set; }

        public string ContactNumber { get; set; }

        public string ContactAddress { get; set; }

        public string LogoName { get; set; }
        public string TripType { get; set; }

        public string FlightStatus { get; set; }
    }
}
