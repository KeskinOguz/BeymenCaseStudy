using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace CommonModule
{
    public abstract class RabbitMQConsumer<T> : BackgroundService where T : IIntegrationEvent
    {
        private readonly ConnectionFactory _factory;
        private const string _exchangeName = "beymencs_exchange";
        private IConnection? _connection;
        private IChannel? _channel;
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQConsumer(IServiceProvider serviceProvider)
        {
            _factory = new ConnectionFactory { HostName = "rabbitmq" };
            _serviceProvider = serviceProvider;
        }

        protected abstract Task HandleMessageAsync(T message,IServiceProvider serviceProvider);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _connection = await _factory.CreateConnectionAsync(stoppingToken);
            _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await _channel.ExchangeDeclareAsync(_exchangeName, ExchangeType.Topic, durable: true, cancellationToken: stoppingToken);

            string queueName = $"{typeof(T).Name}_{this.GetType().Name}";

            await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);

            await _channel.QueueBindAsync(queueName, _exchangeName, typeof(T).Name, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));

                if (message != null)
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        await HandleMessageAsync(message, scope.ServiceProvider);
                    }
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            };

            await _channel.BasicConsumeAsync(queueName, autoAck: false, consumer: consumer, cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel != null) await _channel.CloseAsync(cancellationToken);
            if (_connection != null) await _connection.CloseAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }
    }
}
