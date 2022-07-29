using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AirlineServices.Controllers;
using AirlineServices.Database;

namespace AirlineServices
{
    public class QueueConsumer
    {
        DatabaseContext _context;
        public int busineesseats;
        public int nonbusineesseats;
        public int flightid;
      

        public static void Consume(IModel channel)
        {
            QueueConsumer queueConsumer = new QueueConsumer();


            string[] msg;
           
            channel.QueueDeclare("demo-queue2",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                msg = message.Split(',');
                queueConsumer.busineesseats = Convert.ToInt32( msg[0].Substring(1));
                queueConsumer.nonbusineesseats = Convert.ToInt32(msg[1].Substring(0, msg[1].Length-1));
                queueConsumer.flightid = Convert.ToInt32(msg[2].Substring(0, msg[2].Length - 1));
             
                Console.WriteLine(message);
            };

            //var entity = QueueConsumer._context.Airline_Master.Find(flightid);

            //entity.Business_Seats = busineesseats;
            //entity.Non_Business_Seats = nonbusineesseats;

            //_context.SaveChanges();
            channel.BasicConsume("demo-queue2", true, consumer);
          

        }
    }
}
