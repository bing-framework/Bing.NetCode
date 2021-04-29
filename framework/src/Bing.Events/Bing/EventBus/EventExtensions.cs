using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Bing.EventBus
{
    /// <summary>
    /// 事件扩展
    /// </summary>
    public static class EventExtensions
    {
        /// <summary>
        /// 事件类型字典
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private static readonly ConcurrentDictionary<Type, bool> EventTypes;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static EventExtensions() => EventTypes = new ConcurrentDictionary<Type, bool>();

        /// <summary>
        /// 是否事件类型
        /// </summary>
        /// <param name="eventType">类型</param>
        public static bool IsEvent(this Type eventType) =>
            EventTypes.GetOrAdd(eventType, type =>
            {
                if (typeof(Event).IsAssignableFrom(type))
                    return true;
                return type.GetCustomAttribute<EventAttribute>() != null &&
                       type.GetProperty(nameof(MessageEvent.EventId)) != null &&
                       type.GetProperty(nameof(MessageEvent.EventTime)) != null;
            });

        /// <summary>
        /// 获取处理事件方法
        /// </summary>
        /// <param name="handlerType">处理器类型</param>
        /// <param name="eventType">事件类型</param>
        public static MethodInfo GetHandlerMethod(this Type handlerType, Type eventType)
        {
            var type = typeof(IEventHandler<>).MakeGenericType(eventType);
            return type.IsAssignableFrom(handlerType) ? type.GetMethod("HandleAsync", new[] {eventType}) : null;
        }
    }
}
