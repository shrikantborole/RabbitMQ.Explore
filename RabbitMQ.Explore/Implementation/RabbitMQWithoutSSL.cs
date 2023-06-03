using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Explore.Interface;
using RabbitMQ.Explore.Models;
using System;

namespace RabbitMQ.Explore.Implementation
{
    public class RabbitMQWithoutSSL : IRabbitMQ
    {
        private readonly IOptions<RabbitMQOptions> _rabbitMQOptions;
        private readonly ILogger _logger;

        public RabbitMQWithoutSSL(IOptions<RabbitMQOptions> rabbitMQOptions, ILogger logger)
        {
            _rabbitMQOptions = rabbitMQOptions;
            _logger = logger;
        }
        public void Connect()
        {
            try
            {
                string rabbitmqHostName = _rabbitMQOptions.Value.HostName;
                string rabbitmqUsername = _rabbitMQOptions.Value.UserName;
                string rabbitmqPassword = _rabbitMQOptions.Value.Password;

                var factory = new ConnectionFactory();
                factory.Uri = new Uri($"amqp://{rabbitmqUsername}:{rabbitmqPassword}@{rabbitmqHostName}");

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        _logger.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully connected and opened a channel");
                        channel.QueueDeclare("rabbitmq-dotnet-test", false, false, false, null);
                        _logger.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully declared a queue");
                        channel.QueueDelete("rabbitmq-dotnet-test");
                        _logger.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully deleted a queue");
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                _logger.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - {error}");
            }
        }
    }
}
