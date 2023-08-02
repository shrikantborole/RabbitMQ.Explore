using RabbitMQ.Tls.Mtls.Explore.Interface;

namespace RabbitMQ.Tls.Mtls.Explore
{
    public class RabbitMQStartup
    {
        private readonly IRabbitMQ _iRabbitMQ;
        public RabbitMQStartup(IRabbitMQ iRabbitMQ)
        {
            _iRabbitMQ = iRabbitMQ;
        }
        public void Run()
        {
            _iRabbitMQ.Connect();
        }
    }
}
