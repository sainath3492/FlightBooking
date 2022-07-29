using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Booking
{
    public class QueueProducer
    {
        public static void Publish(IModel channel, int bussinessseats, int normalseats,int Flightid)
        {
            channel.QueueDeclare("demo-queue2",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var count = 0;

            //while (true)
            //{
                var message = bussinessseats +"," + normalseats +"," + Flightid;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "demo-queue2", null, body);
                count++;
                Thread.Sleep(1000);
           // }
        }
    }
}
