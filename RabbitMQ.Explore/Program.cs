using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Explore.Logging;
using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;

namespace RabbitMQ.Explore
{
    public class Program
    {
        private readonly IConfigurationBuilder configurationBuilder;

        /// <summary>
        /// Ctor
        /// </summary>
        public Program()
        {
            configurationBuilder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        }

        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Program program = new Program();
            //program.RabbitMQWithoutSSLEnable();
            program.RabbitMQWithSSLEnable();
            Console.WriteLine();
            Console.WriteLine("Press Enter to Exit !!");
            Console.ReadLine();
        }

        /// <summary>
        /// RabbitMQ Without SSL is Enabled
        /// </summary>
        private void RabbitMQWithoutSSLEnable()
        {
            try
            {
                var parentSection = configurationBuilder.Build().GetSection("RabbitMQ");
                string rabbitmqHostName = parentSection.GetSection("HostName").Value;
                string rabbitmqUsername = parentSection.GetSection("UserName").Value;
                string rabbitmqPassword = parentSection.GetSection("Password").Value;

                var factory = new ConnectionFactory();
                factory.Uri = new Uri($"amqp://{rabbitmqUsername}:{rabbitmqPassword}@{rabbitmqHostName}");

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully connected and opened a channel");
                        Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully connected and opened a channel");
                        channel.QueueDeclare("rabbitmq-dotnet-test", false, false, false, null);
                        Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully declared a queue");
                        Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully declared a queue");
                        channel.QueueDelete("rabbitmq-dotnet-test");
                        Console.WriteLine("Successfully deleted the queue");
                        Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully deleted a queue");
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.ToString();
                Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - {error}");
                System.Console.WriteLine(error);
            }
        }

        /// <summary>
        /// RabbitMQ Code with SSL is enabled
        /// </summary>
        private void RabbitMQWithSSLEnable()
        {
            try
            {
                var parentSection = configurationBuilder.Build().GetSection("RabbitMQ");
                string rabbitmqHostName = parentSection.GetSection("HostName").Value;
                string rabbitmqServerName = parentSection.GetSection("ServerName").Value;
                string certificateFilePath = parentSection.GetSection("CertPath").Value;
                string certificatePassphrase = parentSection.GetSection("CertPassphrase").Value;
                string rabbitmqUsername = parentSection.GetSection("UserName").Value;
                string rabbitmqPassword = parentSection.GetSection("Password").Value;
                bool.TryParse(parentSection.GetSection("MTLSEnabled").Value, out bool mTLSEnabled);
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
                        Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully connected and opened a channel");
                        Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully connected and opened a channel");
                        channel.QueueDeclare("rabbitmq-dotnet-test", false, false, false, null);
                        Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully declared a queue");
                        Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully declared a queue");
                        channel.QueueDelete("rabbitmq-dotnet-test");
                        Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully deleted the queue");
                        Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - Successfully deleted a queue");
                    }
                }
            }
            catch (System.Exception ex)
            {
                var error = ex.ToString();
                Logger.LogWriter($"{System.Reflection.MethodBase.GetCurrentMethod().Name} - {error}");
                System.Console.WriteLine(error);
            }
        }
    }
}
