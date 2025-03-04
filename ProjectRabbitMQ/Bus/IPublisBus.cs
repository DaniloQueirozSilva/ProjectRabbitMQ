using MassTransit;

namespace ProjectRabbitMQ.Bus
{
    public interface IPublishBus
    {
        Task PublishAsync<T>(T message,CancellationToken ct = default ) where T : class;
    }



    public class PublishBus : IPublishBus
    {
        private readonly IPublishEndpoint _busEnpoint;


        public PublishBus(IBusControl publish)
        {
            _busEnpoint = publish;
        }
        public Task PublishAsync<T>(T message, CancellationToken ct = default) where T : class
        {
            
            return _busEnpoint.Publish(message);
        }
    }
}
