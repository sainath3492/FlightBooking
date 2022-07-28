using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AirlineServices
{
    public class QueueConsumer
    {
        public static void Consume(IModel channel)
        {
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
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-queue2", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();

        }
    }
}
