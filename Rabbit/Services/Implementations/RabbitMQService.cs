using Rabbit.CustomDI.ServiceImplementation;
using Rabbit.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace Rabbit.Services.Implementations
{
    public class RabbitMQService : IRabbitMQService, IScopedService
    {
        private readonly IConfiguration _configuration;
        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void GetMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            GetMessage(message);
        }

        public async Task<string> GetMessage()
        {
            var result = "";
            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetSection("RabbitMQ:HostName").Value,
                UserName = _configuration.GetSection("RabbitMQ:UserName").Value,
                Password = _configuration.GetSection("RabbitMQ:Password").Value,
                Port = int.Parse(_configuration.GetSection("RabbitMQ:Port").Value)

            };
            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: "MyQueue",
                               durable: true,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] Received {message}");

                    result = message;
                    await channel.BasicAckAsync(ea.DeliveryTag, false);
                    Console.WriteLine($" [✓] Processed {message}");
                    
                };
                await channel.BasicConsumeAsync("MyQueue", autoAck: false, consumer: consumer);

                return result;

            }
        }
    }
}
