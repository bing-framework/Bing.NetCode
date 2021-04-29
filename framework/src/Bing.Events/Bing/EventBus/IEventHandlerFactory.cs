using System;
using System.Collections.Generic;

namespace Bing.EventBus
{
    /// <summary>
    /// 事件处理器工厂
    /// </summary>
    public interface IEventHandlerFactory
    {
        /// <summary>
        /// 创建事件处理器
        /// </summary>
        /// <param name="handlerType">事件处理器类型</param>
        IEnumerable<object> Create(Type handlerType);
    }
}
