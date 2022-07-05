using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Shared.Infra.Event.EventConsumer
{
    /// <summary>
    /// Interface to Event Consumer.
    /// </summary>
    public interface IEventConsume
    {
        /// <summary>
        /// Start consume event.
        /// </summary>
        /// <returns></returns>
        Task StartProcessingAsync();

        /// <summary>
        /// Stop consume event.
        /// </summary>
        /// <returns></returns>
        Task StopProcessingAsync();

        /// <summary>
        /// Set Logger.
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        Task SetLogger(ILogger<IEventConsume> logger);

    }
}
