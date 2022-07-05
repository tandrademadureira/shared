using Azure.Messaging.EventHubs.Processor;
using System.Threading.Tasks;

namespace Shared.Infra.Event.EventConsumer.EventHubConsumer
{
    /// <summary>
    /// Interface Event Hub consumer.
    /// </summary>
    public interface IEventHubConsume : IEventConsume
    {
        /// <summary>
        /// Initialize partition on Event Hub consumer.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task EventProcessorClient_PartitionInitializingAsync(PartitionInitializingEventArgs arg);

        /// <summary>
        /// Close partition on Event Hub consumer.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task EventProcessorClient_PartitionClosingAsync(PartitionClosingEventArgs arg);
    }
}
