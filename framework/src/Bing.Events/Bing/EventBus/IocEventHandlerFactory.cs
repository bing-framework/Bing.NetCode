using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Bing.EventBus
{
    /// <summary>
    /// 基于依赖注入实现的事件处理器工厂
    /// </summary>
    public class IocEventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 服务提供程序
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 初始化一个<see cref="IocEventHandlerFactory"/>类型的实例
        /// </summary>
        /// <param name="serviceProvider">服务提供程序</param>
        public IocEventHandlerFactory(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        /// <summary>
        /// 创建事件处理器
        /// </summary>
        /// <param name="handlerType">事件处理器类型</param>
        public IEnumerable<object> Create(Type handlerType)
        {
            using var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetServices(handlerType);
        }
    }
}
