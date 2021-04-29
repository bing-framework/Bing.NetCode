using System;
using System.Threading.Tasks;

namespace Bing.EventBus
{
    /// <summary>
    /// 事件总线
    /// </summary>
    public interface IEventBus : IDisposable
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">事件</param>
        Task PublishAsync(dynamic @event);

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent;

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">事件</param>
        /// <returns>若是事件对象则发布，否则返回 false</returns>
        Task<bool> PublishIfEventAsync(dynamic @event);
    }
}
