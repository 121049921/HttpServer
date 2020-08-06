using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    //没用缓存
    public class ActionMethodSelector
    {
        public MethodInfo[] ActionMethods { get; private set; }
        public Type ControllerType { get; private set; }

        public ActionMethodSelector(Type controllerType)
        {
            ControllerType = controllerType;
            var allMethods = ControllerType.GetMethods(BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public);
            ActionMethods = allMethods;
        }
    }
}
