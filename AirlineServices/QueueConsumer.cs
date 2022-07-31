using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AirlineServices.Controllers;
using AirlineServices.Database;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace AirlineServices
{
    public class QueueConsumer: IHostedService
    {
        DatabaseContext _context;
        public int busineesseats;
        public int nonbusineesseats;
        public int flightid;
        private readonly IModel channel;
        private readonly IConnectionFactory _factory;
        private readonly IServiceProvider _serviceProvider;
      
        public QueueConsumer(IServiceProvider serviceProvider)
        {
            _factory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
            _serviceProvider = serviceProvider;
              channel = _factory.CreateConnection().CreateModel();
        }

     
        public  void Consume()
        {
          


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
                busineesseats = Convert.ToInt32( msg[0].Substring(1));
               nonbusineesseats = Convert.ToInt32(msg[1]);
                flightid = Convert.ToInt32(msg[2].Substring(0, msg[2].Length - 1));

                new Update(_serviceProvider).updateseats(busineesseats, nonbusineesseats, flightid);
            };

            //var entity = QueueConsumer._context.Airline_Master.Find(flightid);

            //entity.Business_Seats = busineesseats;
            //entity.Non_Business_Seats = nonbusineesseats;

            //_context.SaveChanges();
            channel.BasicConsume("demo-queue2", true, consumer);
          

        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Consume();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Consume();
            return Task.CompletedTask;
        }
    }
}
