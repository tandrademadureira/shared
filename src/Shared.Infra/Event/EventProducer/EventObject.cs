using System.Collections.Generic;

namespace Shared.Infra.Event.EventProducer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventObject<T>
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        private EventObject() { }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="headers"></param>
        /// <param name="obj"></param>
        public EventObject(Dictionary<string, string> headers, T obj)
        {
            Headers = headers;
            Object = obj;
        }

        /// <summary>
        /// Event's headers.
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Object.
        /// </summary>
        public T Object { get; set; }
    }
}
