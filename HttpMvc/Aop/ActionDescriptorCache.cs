using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace HttpMvc
{
    public class ActionDescriptorCache
    {
        //线程安全集合
        private static readonly ConcurrentDictionary<MethodInfo, ActionDescriptor> cache;

        static ActionDescriptorCache()
        {

            cache = new ConcurrentDictionary<MethodInfo, ActionDescriptor>();
        }

        public static ActionDescriptor GetActionDescriptor(MethodInfo method)
        {
            return cache.GetOrAdd(method, GetDescriptor);
        }

        public static ActionDescriptor GetDescriptor(MethodInfo method)
        {

            IActionAttribute [] attributes = (ActionAttribute [])method.GetCustomAttributes(typeof(IActionAttribute)).ToArray();

            var actionName = method.Name;

            ActionDescriptor actionDescriptor = new ActionDescriptor()
            {
                Member = method,
                Name = actionName,
                Attributes = attributes
            };
            return actionDescriptor;

        }
    }
}
