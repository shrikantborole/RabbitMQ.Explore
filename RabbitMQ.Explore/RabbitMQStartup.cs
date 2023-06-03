using RabbitMQ.Explore.Interface;

namespace RabbitMQ.Explore
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
