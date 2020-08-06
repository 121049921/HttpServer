using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{

    /// <summary>
    /// 拦截器
    /// </summary>
    public interface IInterceptor
    {
        object Intercept(object target, MethodInfo method, object[] parameters);
    }
}
