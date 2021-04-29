using System;
using System.Threading.Tasks;
using Bing.Helpers;

namespace Bing.EventBus.Local
{
    /// <summary>
    /// 基于内存的本地事件总线
    /// </summary>
    public class LocalEventBus : ILocalEventBus
    {
        /// <summary>
        /// 事件处理器工厂
        /// </summary>
        private readonly IEventHandlerFactory _handlerFactory;

        /// <summary>
        /// 初始化一个<see cref="LocalEventBus"/>类型的实例
        /// </summary>
        /// <param name="handlerFactory">事件处理器工厂</param>
        public LocalEventBus(IEventHandlerFactory handlerFactory) => _handlerFactory = handlerFactory;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">事件</param>
        public virtual async Task PublishAsync(dynamic @event)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent">事件类型</typeparam>
        /// <param name="event">事件</param>
        public virtual async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            Check.NotNull(@event, nameof(@event));
            var eventType = @event.GetType();

        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="event">事件</param>
        /// <returns>若是事件对象则发布，否则返回 false</returns>
        public virtual async Task<bool> PublishIfEventAsync(dynamic @event)
        {
            throw new NotImplementedException();
        }
    }
}
