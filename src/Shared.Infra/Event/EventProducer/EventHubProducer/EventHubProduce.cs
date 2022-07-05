using Microsoft.Extensions.Configuration;

namespace Shared.Infra.Event.EventProducer.EventHubProducer
{
    /// <summary>
    /// Abstract class that produce event into event hub.
    /// </summary>
    public class EventHubProduce : EventHubProduceAbstract, IEventHubProduce
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public EventHubProduce(IConfiguration configuration) :
            base(configuration.GetSection("EventHubConsumer:ConnectionString").Value, configuration.GetSection("EventHubConsumer:Name").Value)
        { }
    }
}
