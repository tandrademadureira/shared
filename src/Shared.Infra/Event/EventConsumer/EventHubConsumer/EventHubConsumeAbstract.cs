using Azure.Messaging.EventHubs;
using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace Shared.Infra.Event.EventConsumer.EventHubConsumer
{
    /// <summary>
    /// Abstract class to consumer Event Hub.
    /// </summary>
    public abstract class EventHubConsumeAbstract
    {
        private readonly EventProcessorClient eventProcessorClient;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="ehubNamespaceConnectionString"></param>
        /// <param name="eventHubName"></param>
        /// <param name="blobStorageConnectionString"></param>
        /// <param name="blobContainerName"></param>
        /// <param name="consumerGroup"></param>
        protected EventHubConsumeAbstract(string ehubNamespaceConnectionString, string eventHubName, string blobStorageConnectionString, string blobContainerName, string consumerGroup)
        {
            // Create a blob container client that the event processor will use 
            var storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);

            // Create an event processor client to process events in the event hub
            eventProcessorClient = new EventProcessorClient(storageClient, consumerGroup, ehubNamespaceConnectionString, eventHubName);

            eventProcessorClient.ProcessEventAsync += EventProcessorClient_ProcessEventAsync;
            eventProcessorClient.ProcessErrorAsync += EventProcessorClient_ProcessErrorAsync;
            eventProcessorClient.PartitionInitializingAsync += EventProcessorClient_PartitionInitializingAsync;
            eventProcessorClient.PartitionClosingAsync += EventProcessorClient_PartitionClosingAsync;
        }

        /// <summary>
        /// Clase partition on Event Hub.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public virtual Task EventProcessorClient_PartitionClosingAsync(Azure.Messaging.EventHubs.Processor.PartitionClosingEventArgs arg)
        {
            //log
            return Task.CompletedTask;
        }

        /// <summary>
        /// Initialize patition on Event Hub.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public virtual Task EventProcessorClient_PartitionInitializingAsync(Azure.Messaging.EventHubs.Processor.PartitionInitializingEventArgs arg)
        {
            //colocar log
            return Task.CompletedTask;
        }

        /// <summary>
        /// Process event hub error.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected abstract Task EventProcessorClient_ProcessErrorAsync(Azure.Messaging.EventHubs.Processor.ProcessErrorEventArgs arg);

        /// <summary>
        /// Process event from Event hub.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        protected abstract Task EventProcessorClient_ProcessEventAsync(Azure.Messaging.EventHubs.Processor.ProcessEventArgs arg);

        /// <summary>
        /// Start process.
        /// </summary>
        /// <returns></returns>
        protected async Task StartProcessingAsync()
        {
            eventProcessorClient.StartProcessing();
        }

        /// <summary>
        /// Stop process.
        /// </summary>
        /// <returns></returns>
        protected async Task StopProcessingAsync()
        {
            eventProcessorClient.StartProcessing();
        }
    }
}
