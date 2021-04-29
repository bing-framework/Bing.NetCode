using System;

namespace Bing.EventBus
{
    /// <summary>
    /// 事件 属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class EventAttribute : Attribute
    {
    }
}
