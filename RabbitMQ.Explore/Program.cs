using RabbitMQ.Client;
using System;
using System.Net.Security;
using System.Security.Authentication;

namespace RabbitMQ.Explore
{
    public class Program
    {
        static void Main(string[] args)
        {
            //RabbitMQWithoutSSLEnable();
            RabbitMQWithSSLEnable();
        }

        /// <summary>
        /// RabbitMQ Without SSL is Enable
        /// </summary>
        private static void RabbitMQWithoutSSLEnable()
        {
            try
            {
                string rabbitmqHostName = "desktop-s08pnk3";
                string rabbitmqUsername = "test";
                string rabbitmqPassword = "test";

                var factory = new ConnectionFactory();
                factory.Uri = new Uri($"amqp://{rabbitmqUsername}:{rabbitmqPassword}@{rabbitmqHostName}");

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        Console.WriteLine("Successfully connected and opened a channel");
                        channel.QueueDeclare("rabbitmq-dotnet-test", false, false, false, null);
                        Console.WriteLine("Successfully declared a queue");
                        channel.QueueDelete("rabbitmq-dotnet-test");
                        Console.WriteLine("Successfully deleted the queue");
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                System.Console.WriteLine(error);
            }
        }

        /// <summary>
        /// RabbitMQ Code with SSL is enabled
        /// </summary>
        private static void RabbitMQWithSSLEnable()
        {
            try
            {
                string rabbitmqHostName = "desktop-s08pnk3";
                string rabbitmqServerName = "desktop-s08pnk3";
                string certificateFilePath = @"C:\temp\ThirdOne\client\client_certificate.pem";
                string certificatePassphrase = ""; // "MySecretPassword";
                string rabbitmqUsername = "test";
                string rabbitmqPassword = "test";

                var factory = new ConnectionFactory();

                //factory.HostName = rabbitmqHostName;
                //factory.UserName = rabbitmqUsername;
                //factory.Password = rabbitmqPassword;

                factory.Uri = new Uri($"amqps://{rabbitmqUsername}:{rabbitmqPassword}@{rabbitmqHostName}");


                // Note: This should NEVER be "localhost"
                factory.Ssl.ServerName = rabbitmqServerName;

                // Path to my .p12 file.
                //factory.Ssl.CertPath = certificateFilePath;
                // Passphrase for the certificate file - set through OpenSSL
               // factory.Ssl.CertPassphrase = certificatePassphrase;
                factory.Ssl.Enabled = true;

                // Make sure TLS 1.2 is supported & enabled by your operating system
                factory.Ssl.Version = SslProtocols.Tls12;

                // This is the default RabbitMQ secure port
                factory.Port = AmqpTcpEndpoint.UseDefaultPort;
                factory.VirtualHost = "/";
                factory.Ssl.AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateChainErrors | SslPolicyErrors.RemoteCertificateNameMismatch | SslPolicyErrors.RemoteCertificateNotAvailable;

                //System.Net.ServicePointManager.Expect100Continue = false;


                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        Console.WriteLine("Successfully connected and opened a channel");
                        channel.QueueDeclare("rabbitmq-dotnet-test", false, false, false, null);
                        Console.WriteLine("Successfully declared a queue");
                        channel.QueueDelete("rabbitmq-dotnet-test");
                        Console.WriteLine("Successfully deleted the queue");
                    }
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.ToString();
                System.Console.WriteLine(error);
            }
        }
    }
}
