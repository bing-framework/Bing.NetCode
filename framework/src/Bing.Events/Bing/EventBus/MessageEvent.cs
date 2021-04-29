using System;
using System.Text;
using Bing.EventBus.Properties;
using Bing.Utils.Json;

namespace Bing.EventBus
{
    /// <summary>
    /// 消息事件
    /// </summary>
    public class MessageEvent : Event, IMessageEvent
    {
        /// <summary>
        /// 事件源标识
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 事件发生时间
        /// </summary>
        public long EventTime { get; set; }

        /// <summary>
        /// 消息名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 事件数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 回调名称
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        /// 是否立即发送消息
        /// </summary>
        public bool Send { get; set; }

        /// <summary>
        /// 初始化一个<see cref="MessageEvent"/>类型的实例
        /// </summary>
        public MessageEvent()
        {
            EventId = Helpers.Id.Guid();
            EventTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append($"{EventBusResources.EventId}: {EventId},");
            result.Append($"{EventBusResources.EventTime}: {DateTimeOffset.FromUnixTimeMilliseconds(EventTime):yyyy-MM-dd HH:mm:ss.fff},");
            if (string.IsNullOrWhiteSpace(Name) == false)
                result.Append($"{EventBusResources.MessageName}: {Name},");
            if (string.IsNullOrWhiteSpace(Callback) == false)
                result.Append($"{EventBusResources.Callback}: {Callback},");
            result.Append($"{EventBusResources.EventData}: {JsonHelper.ToJson(Data)}");
            return result.ToString();
        }
    }
}

