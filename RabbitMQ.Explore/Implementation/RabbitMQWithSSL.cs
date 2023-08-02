using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Tls.Mtls.Explore.Interface;
using RabbitMQ.Tls.Mtls.Explore.Models;
using System;
using System.Net.Security;
using System.Security.Authentication;

namespace RabbitMQ.Tls.Mtls.Explore.Implementation
{
    public class RabbitMQWithSSL : IRabbitMQ
    {

        private readonly IOptions<RabbitMQOptions> _rabbitMQOptions;
        private readonly ILogger _logger;

        public RabbitMQWithSSL(IOptions<RabbitMQOptions> rabbitMQOptions, ILogger logger)
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

                string rabbitmqServerName = _rabbitMQOptions.Value.ServerName;
                string certificateFilePath = _rabbitMQOptions.Value.CertPath;
                string certificatePassphrase = _rabbitMQOptions.Value.CertPassphrase;

                bool.TryParse(_rabbitMQOptions.Value.MTLSEnabled, out bool mTLSEnabled);
                var factory = new ConnectionFactory();

                factory.Uri = new Uri($"amqps://{rabbitmqUsername}:{rabbitmqPassword}@{rabbitmqHostName}");


                // Note: This should NEVER be "localhost"
                factory.Ssl.ServerName = rabbitmqServerName;

                if (mTLSEnabled)
                {
                    // Path to my .p12 file.
                    factory.Ssl.CertPath = certificateFilePath;
                    // Passphrase for the certificate file - set through OpenSSL
                    factory.Ssl.CertPassphrase = certificatePassphrase;
                }

                factory.Ssl.Enabled = true;

                // Make sure TLS 1.2 is supported & enabled by your operating system
                factory.Ssl.Version = SslProtocols.Tls12;

                // This is the default RabbitMQ secure port
                factory.Port = AmqpTcpEndpoint.UseDefaultPort;
                factory.VirtualHost = "/";
                factory.Ssl.AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateChainErrors | SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateNotAvailable;

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
            catch (System.Exception ex)
            {
                var error = ex.ToString();
                _logger.Log($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - {error}");
            }
        }
    }
}
