using CommonModule.Events;
using Polly;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommonModule
{
    public class RabbitMQPublisher
    {
        private readonly ConnectionFactory _factory;
        private const string ExchangeName = "beymencs_exchange";

        public RabbitMQPublisher()
        {
            _factory = new ConnectionFactory() { HostName = "rabbitmq" };
        }

        public async Task Publish<T>(T integrationEvent) where T : IIntegrationEvent
        {
            var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)));

            await retryPolicy.ExecuteAsync(async () =>
            {
                using var connection = await _factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.ExchangeDeclareAsync(exchange: ExchangeName, type: ExchangeType.Topic, durable: true);

                var json = JsonSerializer.Serialize(integrationEvent);
                var body = Encoding.UTF8.GetBytes(json);

                string routingKey = typeof(T).Name;

                await channel.BasicPublishAsync(exchange: ExchangeName, routingKey: routingKey,body: body);

                Console.WriteLine($" [X] Sent async: {routingKey}");
            });
        }
    }
}
