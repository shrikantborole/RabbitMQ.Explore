namespace RabbitMQ.Tls.Mtls.Explore.Models
{
    public class RabbitMQOptions
    {
        public const string SectionName = "RabbitMQ";
        public string HostName { get; set; }
        public string ServerName { get; set; }
        public string CertPath { get; set; }
        public string CertPassphrase { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MTLSEnabled { get; set; }
        public string SSLEnabled { get; set; }
    }
}