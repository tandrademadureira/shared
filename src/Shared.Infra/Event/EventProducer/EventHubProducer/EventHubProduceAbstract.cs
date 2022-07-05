using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Infra.Event.EventProducer.EventHubProducer
{
    /// <summary>
    /// Abstract class that produce event into event hub.
    /// </summary>
    public abstract class EventHubProduceAbstract
    {
        private readonly EventHubProducerClient producerClient;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="eventHubName"></param>
        protected EventHubProduceAbstract(string connectionString, string eventHubName)
        {
            producerClient = new EventHubProducerClient(connectionString, eventHubName);
        }

        /// <summary>
        /// Produce event into Event Hub.
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> ProduceAsync(Dictionary<string, string> keyValuePairs, object obj)
        {
            using (EventDataBatch eventBatch = await producerClient.CreateBatchAsync())
            {
                EventObject<object> eventObject = new EventObject<object>(keyValuePairs, obj);

                if (!eventBatch.TryAdd(new EventData(Newtonsoft.Json.JsonConvert.SerializeObject(eventObject))))
                    return false;

                await producerClient.SendAsync(eventBatch);

                return true;
            }
        }

        /// <summary>
        /// Dispose Event Hub.
        /// </summary>
        /// <returns></returns>
        public async Task DisposeAsync()
        {
            await producerClient.DisposeAsync();
        }
    }
}
