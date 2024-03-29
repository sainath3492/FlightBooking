﻿using AirlineServices.Database;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpGet("/api/v1.0/flight/airline/Airline_List")]
        public IEnumerable<AddAirline> Airline_List()
        {


            try
            {
                var data = _context.Airline_Details.ToList();
              
                return data;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }

        }

        [HttpGet("/api/v1.0/flight/airline/FlightListByID")]
        public IEnumerable<Airline> FlightListByID(int ID)
        {


            try
            {
                var data = _context.Airline_Master.Where(u => (u.FK_AirlineID == ID)).ToList();

                return data;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }

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
       
        public AddAirline register(AddAirline addAirline)
        {
           

            try
            {


                string[] filedata = addAirline.Logoimage.Split(",");
                byte[] imageBytes = Convert.FromBase64String(filedata[1]);

                //Save the Byte Array as Image File.
                string filePath = "C:/Users/cogdotnet1094/source/repos/FlightServices/src/FileUpload/" + addAirline.AirlineName + ".png";
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                System.IO.File.WriteAllBytes(filePath, imageBytes);

                //File.WriteAllBytes(filePath, imageBytes);
                _context.Airline_Details.Add(addAirline);
                _context.SaveChanges();
                var data = _context.Airline_Details.ToList().LastOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }

        }

        [HttpPost("/api/v1.0/flight/airline/inventory/add"), Authorize(Roles = "Admin")]
    
        public Airline add(Airline flight_Bookings)
        {

            try {

                _context.Airline_Master.Add(flight_Bookings);
                _context.SaveChanges();
                var data = _context.Airline_Master.ToList().LastOrDefault();
                return data;
                //var entity = _context.Airline_Master.Find(flight_Bookings.AirlineID);

                //entity.From_Place = flight_Bookings.From_Place;
                //entity.To_Place = flight_Bookings.To_Place;
                //entity.Start_Date = flight_Bookings.Start_Date;
                //entity.End_Date = flight_Bookings.End_Date;
                //entity.Scheduled_Days = flight_Bookings.Scheduled_Days;
                //entity.Instrument = flight_Bookings.Instrument;
                //entity.Business_Seats = flight_Bookings.Business_Seats;
                //entity.Non_Business_Seats = flight_Bookings.Non_Business_Seats;
                //entity.Ticket_Cost = flight_Bookings.Ticket_Cost;
                //entity.Rows = flight_Bookings.Rows;
                //entity.Meal = flight_Bookings.Meal;
                //_context.SaveChanges();
                //return flight_Bookings.AirlineID;
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                return null;
            }
           

        }
        [Authorize]
        [HttpPost("/api/v1.0/flight/airline/BlockAirline")]
     
        public string BlockAirline(Status status)
        {
            try
            {
                var entity = _context.Airline_Details.Find(status.AirlineID);

                entity.Status = status.StatusValue;

                _context.SaveChanges();

              var  entity1= _context.Airline_Master.Where(u => (u.FK_AirlineID == status.AirlineID)).ToList();
               for(int i = 0; i < entity1.Count; i++)
                {
                    entity1[i].FlightStatus= status.StatusValue;
                    _context.SaveChanges();
                }
               
                return "Blocked Succesfully";

            }
            catch (Exception ex)
            {
                return "Internal Server Error";
            }
        }
        [HttpPost("/api/v1.0/flight/airline/UpdateSeats")]

        public string UpdateSeats(UpdateSeats update)
        {
            try
            {
                var entity = _context.Airline_Master.Find(update.AirlineID);

                entity.Business_Seats = update.BusinessseatsCount;
                entity.Non_Business_Seats = update.Non_BusinessseatsCount;

                _context.SaveChanges();
                return "Updated Succesfully";

            }
            catch (Exception ex)
            {
                return "Internal Server Error";
            }
        }

        [HttpGet("/api/v1.0/flight/airline/UpdateMQSeats")]
        public string UpdateMQSeats(int busineesseats, int nonbusineesseats , int flightid)
        {
            try
            {
                var entity = _context.Airline_Master.Find(flightid);

                entity.Business_Seats = busineesseats;
                entity.Non_Business_Seats = nonbusineesseats;

                _context.SaveChanges();
                return "Updated Succesfully";

            }
            catch (Exception ex)
            {
                return "Internal Server Error";
            }
        }


    }
}
