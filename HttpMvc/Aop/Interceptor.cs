using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public class Interceptor : IInterceptor
    {
        public virtual object Intercept(object target, MethodInfo method, object[] parameters)
        {
            var actionDescripter = this.GetActionDescriptor(method, parameters);
            return actionDescripter;
        }


        public virtual ActionDescriptor GetActionDescriptor(MethodInfo method, object[] parameters)
        {
            var cache = ActionDescriptorCache.GetActionDescriptor(method);
            var actionDescripter = cache.Clone() as ActionDescriptor;

            for (var i = 0; i < actionDescripter.Parameters.Length; i++)
            {
                //actionDescripter.Parameters[i].Value = parameters[i];
            }
            return actionDescripter;


        }
    }
}
