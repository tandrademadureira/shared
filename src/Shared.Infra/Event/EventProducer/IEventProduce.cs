using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.Infra.Event.EventProducer
{
    /// <summary>
    /// Interface to produce event to be consumed.
    /// </summary>
    public interface IEventProduce
    {
        /// <summary>
        /// Produce event.
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task<bool> ProduceAsync(Dictionary<string, string> keyValuePairs, object obj);

        /// <summary>
        /// Dispose producer.
        /// </summary>
        /// <returns></returns>
        Task DisposeAsync();
    }
}
